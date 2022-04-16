﻿namespace Football.Controllers
{
    using Football.Core.Contracts;
    using Football.Core.Models.Leagues;
    using Football.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LeaguesController : Controller
    {
        private readonly ILeagueService leagues;
        private readonly IManagerService managers;

        private readonly IWebHostEnvironment webHostEnvironment;

        public LeaguesController(
            ILeagueService _leagues,
            IManagerService _managers,
            IWebHostEnvironment _webHostEnvironment)
        {
            this.leagues = _leagues;
            this.managers = _managers;
            this.webHostEnvironment = _webHostEnvironment;
        }

        public IActionResult All([FromQuery] AllLeaguesQueryModel query)
        {
            var queryResult = this.leagues.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllLeaguesQueryModel.LeaguesPerPage);

            var leagueNames = this.leagues.AllNames();

            query.Names = leagueNames;
            query.TotalLeagues = queryResult.TotalLeagues;
            query.Leagues = queryResult.Leagues;

            return View(query);
        }

        [Authorize]

        public IActionResult Mine()
        {
            var myLeagues = this.leagues.ByUser(this.User.Id());

            return View(myLeagues);
        }

        [Authorize]

        public IActionResult Add()
        {
            if (!this.managers.IsManager(this.User.Id()))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            return View(new LeagueFormModel
            {
                Teams = this.leagues.AllTeams()
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(LeagueFormModel league)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            if (managerId.Equals(Guid.Empty))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            if (!this.leagues.TeamExists(league.TeamId))
            {
                this.ModelState.AddModelError(nameof(league.TeamId), "Team does not exist.");
            }

            if (ModelState.IsValid)
            {
                league.Teams = this.leagues.AllTeams();

                return View(league);
            }

            string stringFileName = UploadFile(league);

            this.leagues.Create(
                league.Name,
                stringFileName,
                league.Description,
                league.TeamId,
                managerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]

        public IActionResult Edit(Guid id)
        {
            var userId = this.User.Id();

            if (!this.managers.IsManager(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            var league = this.leagues.Details(id);

            if (league.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new LeagueFormModel
            {
                Name = league.Name,
                //Image = league.Image
                Description = league.Description,
                TeamId = league.TeamId,
                Teams = this.leagues.AllTeams(),
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Edit(Guid id, LeagueFormModel league)
        {
            var managerId = this.managers.IdByUser(
                this.User.Id());

            string stringFileName = UploadFile(league);

            if (managerId.Equals(Guid.Empty) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            if (!this.leagues.TeamExists(league.TeamId))
            {
                this.ModelState.AddModelError(nameof(league.TeamId), "Team does not exist.");
            }

            if (ModelState.IsValid)
            {
                league.Teams = this.leagues.AllTeams();

                return View(league);
            }

            if (!this.leagues.IsByManager(id, managerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.leagues.Edit(
                id,
                league.Name,
                stringFileName,
                league.Description,
                league.TeamId);

            return RedirectToAction(nameof(All));
        }

        private string UploadFile(LeagueFormModel model)
        {
            string fileName = null;
            if (model.Image != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "img");
                fileName = Guid.NewGuid().ToString() + "-" + model.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
