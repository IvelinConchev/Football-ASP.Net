namespace Football.Controllers
{
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Players;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : Controller
    {
        private readonly FootballDbContext data;

        public PlayersController(FootballDbContext _data)
        {
            this.data = _data;
        }

        public IActionResult Add() => View(new AddPlayerFormModel
        {
            Positions = this.GetPlayerPositions()
        });

        public IActionResult All()
        {
            var players = this.data
                .Players
                .OrderByDescending(p => p.Id)
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    Team = p.Team,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age,
                    Nationality = p.Nationality,
                    Position = p.Position.Name
                })
                .ToList();

            return View(players);
        }

        [HttpPost]
        public IActionResult Add(AddPlayerFormModel player)
        {
            if (!this.data.Positions.Any(p => p.Id == player.PositionId))
            {
                this.ModelState.AddModelError(nameof(player.PositionId), "Position does not exist");
            }

            if (ModelState.IsValid)
            {
                player.Positions = this.GetPlayerPositions();

                return View(player);
            }

            var playerData = new Player
            {
                FirstName = player.FirstName,
                MiddleName = player.MiddleName,
                LastName = player.LastName,
                Team = player.Team,
                Weight = player.Weight,
                Height = player.Height,
                Goal = player.Goal,
                Age = player.Age,
                ImageUrl = player.ImageUrl,
                Nationality = player.Nationality,
                ShirtNumber = player.ShirtNumber,
                Description = player.Description,
                PositionId = player.PositionId,
            };

            this.data.Players.Add(playerData);

            this.data.SaveChanges();

            //return RedirectToAction("Index", "Home");

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<PlayerPositionViewModel> GetPlayerPositions()
       => this.data
            .Positions
            .OrderBy(p => p.Name)
            .Select(p => new PlayerPositionViewModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();
    }
}
