﻿namespace Football.Controllers
{
    using Football.Core.Constants;
    using Football.Core.Models.Positions;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Data.Models;
    using Football.Models.Positions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PositionsController : Controller
    {
        private readonly FootballDbContext data;

        public PositionsController(FootballDbContext _data)
        {
            this.data = _data;
        }

        [Authorize]
        public async Task<IActionResult> Add() => View(new PositionFormModel
        {

        });

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
