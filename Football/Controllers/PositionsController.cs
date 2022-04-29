namespace Football.Controllers
{
    using Football.Core.Constants;
    using Football.Core.Contracts;
    using Football.Core.Models.Positions;
    using Football.Extensions;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Positions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PositionsController : Controller
    {
        private readonly FootballDbContext data;
        private readonly IManagerService managers;

        public PositionsController(
            FootballDbContext _data,
            IManagerService _мanagers)
        {
            this.data = _data;
            this.managers = _мanagers;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            if (!this.managers.IsManager(this.User.Id()))
            {
                return RedirectToAction(nameof(ManagersController.Become),
                    "Managers");
            }

            return View(new PositionFormModel
            {
            });
        }

        [HttpPost]
        public IActionResult Add(AddPositionFormModel position)
        {
            if (!ModelState.IsValid)
            {
                return View(position);
            }

            var positionData = new Position
            {
                Name = position.Name,
            };

            this.data.Positions.Add(positionData);

            this.data.SaveChanges();

            ViewData[MessageConstant.SuccessMessage] = "Успешен запис";

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
