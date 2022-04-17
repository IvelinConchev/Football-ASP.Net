namespace Football.Controllers
{
    using Football.Core.Contracts;
    using Football.Core.Models.Stadiums;
    using Football.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class StadiumsController : Controller
    {
        private readonly IStadiumService stadiums;
        private readonly IManagerService managers;

        private readonly IWebHostEnvironment webHostEnvironment;

        public StadiumsController(
            IStadiumService _stadiums,
            IManagerService _managers,
            IWebHostEnvironment _webHostEnvironment)
        {
            this.stadiums = _stadiums;
            this.managers = _managers;
            this.webHostEnvironment = _webHostEnvironment;
        }

        public IActionResult All([FromQuery] AllStadiumsQueryModel query)
        {
            var queryResult = this.stadiums.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllStadiumsQueryModel.StadiumsPerPage);

            var stadiumNames = this.stadiums.AllNames();

            query.Names = stadiumNames;
            query.TotalStadiums = queryResult.TotalStadiums;
            query.Stadiums = queryResult.Stadiums;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myStadiums = this.stadiums.ByUser(this.User.Id());

            return View(myStadiums);
        }

        [Authorize]

        public IActionResult Add()
        {
            if (!this.managers.IsManager(this.User.Id()))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            return View(new StadiumFormModel
            {
                Cities = this.stadiums.AllCities()
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(StadiumFormModel stadium)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            if (managerId.Equals(Guid.Empty))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            if (!this.stadiums.CityExists(stadium.CityId))
            {
                this.ModelState.AddModelError(nameof(stadium.CityId), "City does not exist.");
            }

            if (ModelState.IsValid)
            {
                stadium.Cities = this.stadiums.AllCities();

                return View(stadium);
            }

            string stringFileName = UploadFile(stadium);

            this.stadiums.Create(
                stadium.Name,
                stringFileName,
                stadium.Description,
                stadium.Capacity,
                stadium.Address,
                stadium.CityId,
                managerId);

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

            var stadium = this.stadiums.Details(id);

            if (stadium.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new StadiumFormModel
            {
                Name = stadium.Name,
                //Image = stadium.Image,
                Description = stadium.Description,
                Capacity = stadium.Capacity,
                Address = stadium.Address,
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Edit(Guid id, StadiumFormModel stadium)
        {
            var managerId = this.managers.IdByUser(
                this.User.Id());

            string stringFileName = UploadFile(stadium);

            if (managerId.Equals(Guid.Empty) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become
                    ), "Managers");
            }

            if (!this.stadiums.CityExists(stadium.CityId))
            {
                this.ModelState.AddModelError(nameof(stadium.CityId), "City does not exist.");
            }

            if (ModelState.IsValid)
            {
                stadium.Cities = this.stadiums.AllCities();

                return View(stadium);
            }

            if (!this.stadiums.IsByManager(id, managerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.stadiums.Edit(
                id,
                stadium.Name,
                stringFileName,
                stadium.Description,
                stadium.Capacity,
                stadium.Address,
                stadium.CityId);

            return RedirectToAction(nameof(All));
        }

        private string UploadFile(StadiumFormModel model)
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
