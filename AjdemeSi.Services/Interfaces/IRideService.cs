﻿using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Ride;
using System;
using System.Collections.Generic;

namespace AjdemeSi.Services
{
    public interface IRideService
    {
        RidePagedListViewModel GetAll(DateTime? dateFrom, DateTime? dateTo, string countryFrom, string cityFrom, string countryTo, string cityTo, string sortOrder = null, int pageIndex = 1, int countOnPage = 10);
        RidePagedListViewModel GetDriverActiveRides(string driverId, string sortOrder = null, int pageIndex = 1, int countOnPage = 10);

        //AspNetUser GetUser(string id);
        //bool UpdateUser(AspNetUser user);
        //bool ConfirmEmail(string id, string email);
        //bool BlockUser(string id, string email);
        //bool ContactExists(string id, string userName, string email);
        //bool RemoveUser(string id);
    }
}
