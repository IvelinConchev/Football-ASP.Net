namespace Football.Controllers
{
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Stadiums;
    using Microsoft.AspNetCore.Mvc;

    public class StadiumsController : Controller
    {
        private readonly FootballDbContext data;

        public StadiumsController(FootballDbContext _data)
        {
            this.data = _data;
        }

        public IActionResult Add() => View(new AddStadiumFormModel
        {
            Cities = this.GetStadiumCities()
        });

        public IActionResult All()
        {
            var stadiums = data
                .Stadiums
                .OrderByDescending(s => s.Id)
                .Select(s => new StadiumListingViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Capacity = s.Capacity,
                    Address = s.Address,
                    Image = s.Image,
                })
                .ToList();

            return View(stadiums);
        }

        [HttpPost]

        public IActionResult Add(AddStadiumFormModel stadium)
        {
            if (!this.data.Cities.Any(c => c.Id == stadium.CityId))
            {
                this.ModelState.AddModelError(nameof(stadium.CityId), "Cities does not exist.");
            }


            if (ModelState.IsValid)
            {
                stadium.Cities = GetStadiumCities();
            }

            var stadiumData = new Stadium
            {
                Name = stadium.Name,
                Address = stadium.Address,
                Capacity = stadium.Capacity,
                Description = stadium.Description,
                Image = stadium.Image,
            };

            this.data.Stadiums.Add(stadiumData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<StadiumCitiesViewModel> GetStadiumCities()
            => this.data
            .Cities
            .OrderBy(c => c.Name)
            .Select(c => new StadiumCitiesViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}
