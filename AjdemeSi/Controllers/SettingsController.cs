using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace AjdemeSi.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IDriverService _driverService;
        private readonly IUserService _userService;

        public SettingsController(ICommonService commonService, IDriverService driverService, IUserService userService)
        {
            _commonService = commonService;
            _driverService = driverService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            string currentUserId = HttpContext.User.Identity.GetUserId();
            SettingsViewModel model = new SettingsViewModel()
            {
                CarsViewModel = _driverService.GetCars(currentUserId),
                UserGeneralViewModel = _userService.GetUserGeneralDetails(currentUserId),
                UserResultsViewModel = _userService.GetUserResultsSettingsViewModel(currentUserId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdateCar(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                int currentCarId = car.Id;

                int newCarId = _driverService.AddOrUpdateCar(car, User.Identity.GetUserId());

                if(currentCarId == 0 && newCarId != 0)
                {
                    return PartialView("~/Views/Settings/_carDetails.cshtml", car);
                }
            }

            return PartialView("~/Views/Settings/_addNewCar.cshtml", car);
        }
    }
}