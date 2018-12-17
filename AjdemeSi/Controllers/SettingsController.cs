using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq;
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
                UserResultsViewModel = _userService.GetUserResultsSettingsViewModel(currentUserId),
            };

            ViewData.Add("VehicleMakesList", _commonService.GetVehicleMakes().Select(vm => new SelectListItem()
            {
                Text = vm,
                Value = vm
            }).ToList());

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

        [AllowAnonymous]
        public ActionResult GetVehicleModels(string make)
        {
            if (!string.IsNullOrEmpty(make))
            {
                var models = _commonService.GetVehicleModels(make);
                return Json(new { results = models }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { results = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}