namespace Football.Controllers
{
    using Football.Core.Contracts;
    using Football.Core.Models.Players;
    using Football.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;
    public class PlayersController : Controller
    {
        private readonly IPlayerService players;
        private readonly IManagerService managers;

        private readonly IWebHostEnvironment webHostEnvironment;

        public PlayersController(
            IPlayerService _players,
            IManagerService _managers,
            IWebHostEnvironment _webHostEnvironment
            )
        {
            this.players = _players;
            this.managers = _managers;
            webHostEnvironment = _webHostEnvironment;
        }

        public IActionResult All([FromQuery]
            AllPlayersQueryModel query)
        {
            var queryResult = this.players.All(
                query.Team,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPlayersQueryModel.PlayersPerPage);

            var playerTeams = this.players.AllTeams();

            query.Teams = playerTeams;
            query.TotalPlayers = queryResult.TotalPlayers;
            query.Players = queryResult.Players;

            return View(query);
        }

        [Authorize]

        public IActionResult Mine()
        {
            var myPlayers = this.players.ByUser(this.User.Id());

            return View(myPlayers);
        }

        [Authorize]

        public IActionResult Add()
        {
            //TODO
            if (!this.managers.IsManager(this.User.Id()))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            return View(new PlayerFormModel
            {
                Positions = this.players.AllPositions()
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(PlayerFormModel player)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            if (managerId.Equals(Guid.Empty))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            if (!this.players.PositionExists(player.PositionId))
            {
                this.ModelState.AddModelError(nameof(player.PositionId), "Position does not exist.");
            }

            if (ModelState.IsValid)
            {
                player.Positions = this.players.AllPositions();

                return View(player);
            }

            string stringFileName = UploadFile(player);

            this.players.Create(
                player.FirstName,
                player.MiddleName,
                player.LastName,
                player.Team,
                player.Age,
                player.Weight,
                player.Height,
                stringFileName,
                player.Goal,
                player.ShirtNumber,
                player.Nationality,
                player.Description,
                player.PositionId,
                managerId);

            TempData[GlobalMessageKey] = "You Player was added successfully!";

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var userId = this.User.Id();

            if (!this.managers.IsManager(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            var player = this.players.Details(id);

            if (player.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new PlayerFormModel
            {
                FirstName = player.FirstName,
                MiddleName = player.MiddleName,
                LastName = player.LastName,
                Team = player.Team,
                Age = player.Age,
                Weight = player.Weight,
                Height = player.Height,
                //Image = player.Image,
                Goal = player.Goal,
                ShirtNumber = player.ShirtNumber,
                Nationality = player.Nationality,
                Description = player.Description,
                PositionId = player.PositionId,
                Positions = this.players.AllPositions()
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Edit(Guid id, PlayerFormModel player)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            string stringFileName = UploadFile(player);

            if (managerId.Equals(Guid.Empty) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            if (!this.players.PositionExists(player.PositionId))
            {
                this.ModelState.AddModelError(nameof(player.PositionId), "Position does not exist.");
            }

            if (ModelState.IsValid)
            {
                player.Positions = this.players.AllPositions();

                return View(player);
            }

            if (!this.players.IsByManager(id, managerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.players.Edit(
                id,
                player.FirstName,
                player.MiddleName,
                player.LastName,
                player.Team,
                player.Age,
                player.Weight,
                player.Height,
                stringFileName,
                player.Goal,
                player.ShirtNumber,
                player.Nationality,
                player.Description,
                player.PositionId);

            TempData[GlobalMessageKey] = "You Player was saved successfully!";

            return RedirectToAction(nameof(All));
        }

        private string UploadFile(PlayerFormModel model)
        {
            string fileName = null;
            if (model.Image != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "img");
                fileName = Guid.NewGuid().ToString() + "-" + model.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
