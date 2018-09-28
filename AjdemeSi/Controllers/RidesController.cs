using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace AjdemeSi.Controllers
{
    [Authorize]
    public class RidesController : Controller
    {
        private readonly IRideService _rideService;
        private readonly IDriverService _driverService;
        private readonly ICitiesService _citiesService;
        private readonly ICommonService _commonService;

        public RidesController(IRideService rideService, IDriverService driverService, ICitiesService citiesService, ICommonService commonService)
        {
            _rideService = rideService;
            _driverService = driverService;
            _citiesService = citiesService;
            _commonService = commonService;
        }

        public ActionResult Index(DateTime? dateFrom, DateTime? dateTo, string countryFrom = "", string cityFrom = "", string countryTo = "", string cityTo = "")
        {
            RideDashboardViewModel model = new RideDashboardViewModel()
            {
                MyRidesListViewModel = _rideService.GetDriverActiveRides(User.Identity.GetUserId()),
                SuggestedRidesListViewModel = _rideService.GetDriverSuggestedRides(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                UserCommonViewModel = new UserResultsSettingsExtendedViewModel(User.IsInRole("Driver")), //todo
                CarDetails = _driverService.GetDefaultCar(User.Identity.GetUserId())
            };

            return View(model);
        }

        public ActionResult ApplyFilters(DateTime dateFrom, DateTime dateTo, string countryFrom = "", string cityFrom = "", string countryTo = "", string cityTo = "")
        {
            RideDashboardViewModel model = new RideDashboardViewModel()
            {
                MyRidesListViewModel = _rideService.GetAll(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                SuggestedRidesListViewModel = _rideService.GetAll(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                UserCommonViewModel = new UserResultsSettingsExtendedViewModel(), //todo
                CarDetails = new CarViewModel() // todo
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRide(string placeFrom, string placeTo, string dateFrom, string timeFrom, string timeDelay, string timeTravel, string timeBreak, string passengersNomber, bool isTotalPrice, float price)
        {
            _rideService.CreateRide(placeFrom, placeTo, dateFrom, timeFrom, timeDelay, timeTravel, timeBreak, passengersNomber, isTotalPrice, price, User.Identity.GetUserId());
            return Json(new { results = "success" });
        }

        public ActionResult GetCities(string term)
        {
            var data = _citiesService.GetCities(term, 5);
            return Json(new { results = data }, JsonRequestBehavior.AllowGet);
        }
    }
}