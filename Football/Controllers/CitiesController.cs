namespace Football.Controllers
{
    using Football.Core.Contracts;
    using Football.Core.Models.Cities;
    using Football.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;
    public class CitiesController : Controller
    {
        private readonly IManagerService managers;
        private readonly ICityService cities;

        private readonly IWebHostEnvironment webHostEnvironment;

        public CitiesController(
            IManagerService _managers,
            ICityService _cities,
            IWebHostEnvironment _webHostEnvironment)
        {
            this.managers = _managers;
            this.cities = _cities;
            this.webHostEnvironment = _webHostEnvironment;
        }

        public IActionResult All([FromQuery]
        AllCityQueryModel query)
        {
            var queryResult = this.cities.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCityQueryModel.CityPerPage);


            var cityNames = this.cities.AllNames();

            query.Names = cityNames;
            query.TotalCities = queryResult.TotalCities;
            query.Cities = queryResult.Cities;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myCities = this.cities.ByUser(this.User.Id());

            return View(myCities);
        }

        public IActionResult Details(Guid id, string information)
        {
            var city = this.cities.Details(id);

            if (information != city.GetInformation())
            {
                return BadRequest();
            }

            return View(city);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.managers.IsManager(this.User.Id()))
            {
                return RedirectToAction(nameof(ManagersController.Become),
                    "Managers");
            }
            return View(new CityFormModel
            {
                Teams = this.cities.AllTeams()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(CityFormModel city)
        {
            var managerId = this.managers.IdByUser(this.User.Id());

            if (managerId.Equals(Guid.Empty))
            {
                return RedirectToAction(nameof(ManagersController.Become), "Managers");
            }

            if (!this.cities.TeamExists(city.TeamId))
            {
                this.ModelState.AddModelError(nameof(city.TeamId),
                    "City does not exist.");
            }

            if (ModelState.IsValid)
            {
                city.Teams = this.cities.AllTeams();

                return View(city);
            }

            string stringFileName = UploadFile(city);

            var cityId = this.cities.Create(
                  city.Name,
                  city.PostCode,
                  stringFileName,
                  city.Description,
                  city.TeamId);

            TempData[GlobalMessageKey] = "You Team was added and is awaiting for approval!";

            return RedirectToAction(nameof(Details), new { id = cityId, information = city.GetInformation() });

        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var userId = this.User.Id();

            if (!this.managers.IsManager(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become),
                    "Managers");
            }

            var city = this.cities.Details(id);

            if (city.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new CityFormModel
            {
                Name = city.Name,
                PostCode = city.PostCode,
                //Image = city.Image,
                Description = city.Description,
                TeamId = city.TeamId,
                Teams = this.cities.AllTeams(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Guid id, CityFormModel city)
        {
            var managerId = this.managers.IdByUser(
                this.User.Id());

            string strinfFileName = UploadFile(city);

            if (managerId.Equals(Guid.Empty) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ManagersController.Become),
                    "Managers");
            }

            if (!this.cities.TeamExists(city.TeamId))
            {
                this.ModelState.AddModelError(nameof(city.TeamId),
                    "Team does not exist.");
            }

            if (ModelState.IsValid)
            {
                city.Teams = this.cities.AllTeams();

                return View(city);
            }

            if (!this.cities.IsByManager(id, managerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = this.cities.Edit(
                  id,
                  city.Name,
                  city.PostCode,
                  strinfFileName,
                  city.Description,
                  city.TeamId,
                  this.User.IsAdmin());

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"You Team was edited {(this.User.IsAdmin() ? string.Empty : " and is await for approval")}!";

            return RedirectToAction(nameof(Details), new { id, information = city.GetInformation() });
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return NotFound();
            }

            var delete = this.cities.Delete(id);

            TempData[DangerMessageKey] = $"You City was deleted!";

            return RedirectToAction(nameof(All));
        }

        private string UploadFile(CityFormModel model)
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

