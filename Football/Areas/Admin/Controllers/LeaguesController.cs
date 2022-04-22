namespace Football.Areas.Admin.Controllers
{
    using Football.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class LeaguesController : BaseController
    {
        private readonly ILeagueService leagues;

        public LeaguesController(ILeagueService _leagues)
        {
            this.leagues = _leagues;
        }

        public IActionResult All()
        {
            var leagues =
                this.leagues
                .All(publicOnly: false)
                .Leagues;

            return View(leagues);
        }
        public IActionResult ChangeVisibility(Guid id)
        {
            this.leagues.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
