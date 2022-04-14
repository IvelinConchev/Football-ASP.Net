namespace Football.Controllers
{
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Cities;
    using Microsoft.AspNetCore.Mvc;

    public class CitiesController : Controller
    {
        private readonly FootballDbContext data;

        public CitiesController(FootballDbContext _data)
        {
            this.data = _data;
        }

        public IActionResult Add() => View(new AddCityFormModel
        {
            Teams = this.GetCityTeams()
        });

        public IActionResult All()
        {
            var cities = data
                .Cities
                .OrderByDescending(c => c.Id)
                .Select(c => new CityListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    PostCode = c.PostCode,
                    Image = c.Image,
                    Desctription = c.Desctription,
                    //Team = c.Name
                })
                .ToList();

            return View(cities);
        }

        [HttpPost]

        public IActionResult Add(AddCityFormModel city)
        {
            if (!this.data.Teams.Any(c => c.Id == city.TeamId))
            {
                this.ModelState.AddModelError(nameof(city.TeamId), "Team does not exist.");
            }

            if (ModelState.IsValid)
            {
                city.Teams = GetCityTeams();
            }

            var cityData = new City
            {
                Name = city.Name,
                Image = city.Image,
                PostCode = city.PostCode,
                Desctription = city.Description,
               //TeamCities = city.Teams.
            };

            this.data.Cities.Add(cityData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<CityTeamsViewModel> GetCityTeams()
            => this.data
            .Teams
            .OrderBy(t => t.Name)
            .Select(t => new CityTeamsViewModel
            {
                Id = t.Id,
                Name = t.Name,
            })
            .ToList();
    }
}
