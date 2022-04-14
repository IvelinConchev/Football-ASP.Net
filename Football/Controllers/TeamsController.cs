namespace Football.Controllers
{
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Teams;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : Controller
    {
        private readonly FootballDbContext data;

        public TeamsController(FootballDbContext _data)
        {
            this.data = _data;
        }

        public IActionResult Add() => View(new AddTeamFormModel
        {
            Players = this.GetTeamPlayers()
        });

        public IActionResult All()
        {
            var teams = data
                .Teams
                .OrderByDescending(t => t.Id)
                .Select(t => new TeamListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Image = t.Image,
                    LogoUrl = t.LogoUrl,
                    HomeKit = t.HomeKit,
                    AwayKit = t.AwayKit,
                    NickName = t.NickName,
                    Description = t.Description,
                    Address = t.Address,
                    HeadCoach = t.HeadCoach,
                    Champion = t.Champion,
                    Cup = t.Cup,
                    Win = t.Win,
                    Defeats = t.Defeats,
                    Player = t.Player.Team
                })
                .ToList();

            return View(teams);
        }

        [HttpPost]
        public IActionResult Add(AddTeamFormModel team)
        {
            if (!this.data.Players.Any(p => p.Id == team.PlayerId))
            {
                this.ModelState.AddModelError(nameof(team.PlayerId), "Player does not exist.");
            }

            if (ModelState.IsValid)
            {
                team.Players = this.GetTeamPlayers();

                return View(team);
            }

            var teamData = new Team
            {
                Name = team.Name,
                Image = team.Image,
                WebSite = team.WebSite,
                LogoUrl = team.LogoUrl,
                HomeKit = team.HomeKit,
                AwayKit = team.AwayKit,
                NickName = team.NickName,
                Description = team.Description,
                Address = team.Address,
                HeadCoach = team.HeadCoach,
                Champion = team.Champion,
                Cup = team.Cup,
                Win = team.Win,
                Defeats = team.Defeats,
                PlayerId = team.PlayerId
            };

            this.data.Teams.Add(teamData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<TeamPlayersViewModel> GetTeamPlayers()
            => this.data
            .Players
            .OrderBy(p => p.Team)
            .Select(p => new TeamPlayersViewModel
            {
                Id = p.Id,
                Name = p.Team
            })
            .ToList();
    }
}
