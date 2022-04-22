namespace Football.Areas.Admin.Controllers
{
    using Football.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class CitiesController : BaseController
    {
        private readonly ICityService cities;

        public CitiesController(ICityService _cities)
        {
            this.cities = _cities;
        }

        public IActionResult All()
        {
            var cities =
                this.cities
                .All(publicOnly: false)
                .Cities;

            return View(cities);
        }
        public IActionResult ChangeVisibility(Guid id)
        {
            this.cities.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
