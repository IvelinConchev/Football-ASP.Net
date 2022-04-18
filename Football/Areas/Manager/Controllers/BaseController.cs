namespace Football.Areas.Manager.Controllers
{
    using Football.Core.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = UserConstants.Roles.Manager)]
    [Area("Manager")]
    public class BaseController : Controller
    {
        
    }
}
