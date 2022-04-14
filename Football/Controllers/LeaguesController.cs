namespace Football.Controllers
{
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Leagues;
    using Microsoft.AspNetCore.Mvc;

    public class LeaguesController : Controller
    {
        private readonly FootballDbContext data;

        public LeaguesController(FootballDbContext _data)
        {
            this.data = _data;
        }

        public IActionResult Add() => View(new AddLeagueFormModel
        {
            Teams = this.GetLeagueTeams()
        });

        public IActionResult All()
        {
            var leagues = this.data
                .Leagues
                .OrderByDescending(l => l.Id)
                .Select(l => new LeagueListingViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Image = l.Image,
                    Description = l.Description,
                })
                .ToList();

            return View(leagues);
        }

        [HttpPost]
        public IActionResult Add(AddLeagueFormModel league)
        {
            if (!this.data.Teams.Any(l => l.Id == league.TeamId))
            {
                this.ModelState.AddModelError(nameof(league.TeamId), "Team does not exist.");
            }

            if (ModelState.IsValid)
            {
                league.Teams = GetLeagueTeams();

                return View(league);
            }

            var leagueData = new League
            {
                Name = league.Name,
                Description = league.Description,
                Image = league.Image,
                TeamId = league.TeamId,
            };

            this.data.Leagues.Add(leagueData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<LeagueTeamsViewModel> GetLeagueTeams()
            => this.data
            .Teams
            .OrderBy(t => t.Name)
            .Select(t => new LeagueTeamsViewModel
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToList();
    }
}
