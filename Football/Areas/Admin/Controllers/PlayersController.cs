namespace Football.Areas.Admin.Controllers
{
    using Football.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : BaseController
    {
        private readonly IPlayerService players;

        public PlayersController(IPlayerService _players)
        {
            this.players = _players;
        }

        public IActionResult All()
        {
            var players = this.players
                .All(publicOnly: false)
                .Players;

            return View(players);
        }

        public IActionResult ChangeVisibility(Guid id)
        {
            this.players.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
