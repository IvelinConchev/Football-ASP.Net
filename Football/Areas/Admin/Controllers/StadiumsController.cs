namespace Football.Areas.Admin.Controllers
{
    using Football.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class StadiumsController : BaseController
    {
        private readonly IStadiumService stadiums;

        public StadiumsController(IStadiumService _stadiums)
        {
            this.stadiums = _stadiums;
        }

        public IActionResult All()
        {
            var stadiums =
                this.stadiums
                .All(publicOnly: false)
                .Stadiums;

            return View(stadiums);
        }
        public IActionResult ChangeVisibility(Guid id)
        {
            this.stadiums.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
