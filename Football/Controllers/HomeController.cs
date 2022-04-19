namespace Football.Controllers
{
    using System.Diagnostics;
    using Football.Core.Constants;
    using Football.Core.Contracts;
    using Football.Infrastructure.Data;
    using Football.Models;
    using Football.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : BaseController
    {
        private readonly ITeamService teams;
        private readonly IStatisticsService statistics;

        private readonly ILogger<HomeController> _logger;
        public HomeController(
            ITeamService _teams,
            ILogger<HomeController> logger,
            IStatisticsService _statistics)
        {
            this.teams = _teams;
            _logger = logger;
            this.statistics = _statistics;
        }

        public IActionResult Index()
        {
            //var totalPlayers = this.data.Players.Count();

            var latestTeam = this.teams.Latest();


            var totalStatistics = this.statistics.Total();

            ViewData[MessageConstant.SuccessMessage] = "Браво";

            return View(new IndexViewModel
            {
                //TotalPlayers = totalStatistics.TotalPlayers,
                TotalTeams = totalStatistics.TotalTeams,
                TotalUsers = totalStatistics.TotalUsers,
                Teams = latestTeam.ToList()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}