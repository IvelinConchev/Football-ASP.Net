namespace Football.Controllers
{
    using System.Diagnostics;
    using Football.Core.Constants;
    using Football.Core.Contracts;
    using Football.Core.Services.Teams.Models;
    using Football.Models;
    using Football.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : BaseController
    {
        private readonly ITeamService teams;
        private readonly IStatisticsService statistics;
        private readonly IMemoryCache cache;

        private readonly ILogger<HomeController> _logger;
        public HomeController(
            ILogger<HomeController> logger,
            ITeamService _teams,
            IStatisticsService _statistics,
            IMemoryCache _cache)
        {
            this.teams = _teams;
            _logger = logger;
            this.statistics = _statistics;
            this.cache = _cache;
        }

        public IActionResult Index()
        {
            //var totalPlayers = this.data.Players.Count();

            const string latestTeamCacheKey = "LatestTeamCacheKey";

            var latestTeams = this.cache.Get<List<LatestTeamServiceModel>>(latestTeamCacheKey);

            if (latestTeams == null)
            {
                latestTeams = this.teams
                .Latest()
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(20));

                this.cache.Set(latestTeamCacheKey, latestTeams, cacheOptions);
            }

            var totalStatistics = this.statistics.Total();

            ViewData[MessageConstant.SuccessMessage] = "Браво";

            return View(new IndexViewModel
            {
                //TotalPlayers = totalStatistics.TotalPlayers,
                TotalTeams = totalStatistics.TotalTeams,
                TotalUsers = totalStatistics.TotalUsers,
                Teams = latestTeams.ToList()
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