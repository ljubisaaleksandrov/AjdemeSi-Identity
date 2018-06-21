﻿using AjdemeSi.Domain.Models.Common;
using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace AjdemeSi.Controllers
{
    [Authorize]
    public class RidesController : Controller
    {
        private readonly IRideService _rideService;

        public RidesController(IRideService rideService)
        {
            _rideService = rideService;
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

        public ActionResult GetCities(string term)
        {
            var data = new List<string>() { "test", "test1", "test11", "test2" };
            return Json(new { results = data.Where(x => x.Contains(term)).ToArray() }, JsonRequestBehavior.AllowGet);
        }
    }
}