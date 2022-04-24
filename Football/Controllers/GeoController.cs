namespace Football.Controllers
{
    using Football.Models.GeoLocation;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class GeoController : Controller
    {
        public async Task<ActionResult> Details()
        {
            GeoLocationViewModel model = new GeoLocationViewModel();
            GeoHelper geoHelper = new GeoHelper();
            var result = await geoHelper.GetGeoInfo();
            model = JsonConvert.DeserializeObject<GeoLocationViewModel>(result);
            //TempData["GeoCode"] = result;

            return View(model);
        }
    }
}
