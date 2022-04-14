namespace Football.Controllers
{
    using Football.Core.Constants;
    using Football.Infrastructure.Data;
    using Football.Models;
    using Football.Models.Players;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    public class HomeController : BaseController
    {
        private readonly FootballDbContext data;

        private readonly ILogger<HomeController> _logger;
        public HomeController(FootballDbContext _data, ILogger<HomeController> logger)
        {
            this.data = _data;
            _logger = logger;
        }


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
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
                .Take(3)
                .ToList();


            ViewData[MessageConstant.SuccessMessage] = "Браво";

            //return View();
            return View(players);
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