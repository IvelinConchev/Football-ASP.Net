namespace Football.Areas.Admin.Controllers
{
    using Football.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : BaseController
    {
        private readonly ITeamService teams;

        public TeamsController(ITeamService _teams)
        {
            this.teams = _teams;
        }

        public IActionResult All()
        {
            var teams =
                this.teams
                .All(publicOnly: false)
                .Teams;

            return View(teams);
        }
        public IActionResult ChangeVisibility(Guid id)
        {
            this.teams.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
