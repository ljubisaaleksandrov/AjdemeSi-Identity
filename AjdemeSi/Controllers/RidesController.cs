using AjdemeSi.Domain.Models.Common;
using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using AjdemeSi.Services.Interfaces;

namespace AjdemeSi.Controllers
{
    [Authorize]
    public class RidesController : Controller
    {
        private readonly IRideService _rideService;
        private readonly ICitiesService _citiesService;

        public RidesController(IRideService rideService, ICitiesService citiesService)
        {
            _rideService = rideService;
            _citiesService = citiesService;
        }

        public ActionResult Index(DateTime? dateFrom, DateTime? dateTo, string countryFrom = "", string cityFrom = "", string countryTo = "", string cityTo = "")
        {
            RideDashboardViewModel model = new RideDashboardViewModel()
            {
                DriverRidePagedListViewModel = _rideService.GetAll(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                RidePagedListViewModel = _rideService.GetAll(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                UserCommonViewModel = new UserCommonViewModel() //todo
            };

            return View(model);
        }

        public ActionResult ApplyFilters(DateTime dateFrom, DateTime dateTo, string countryFrom = "", string cityFrom = "", string countryTo = "", string cityTo = "")
        {
            RideDashboardViewModel model = new RideDashboardViewModel()
            {
                DriverRidePagedListViewModel = _rideService.GetAll(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                RidePagedListViewModel = _rideService.GetAll(dateFrom, dateTo, countryFrom, cityFrom, countryTo, cityTo),
                UserCommonViewModel = new UserCommonViewModel() //todo
            };

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GetCities(string term)
        {
            var data = _citiesService.GetCities(term, 5);
            return Json(new { results = data }, JsonRequestBehavior.AllowGet);
        }
    }
}