using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Settings;
using System;
using System.Collections.Generic;

namespace AjdemeSi.Services.Interfaces
{
    public interface IDriverService
    {
        string AddDriver(string userId, DateTime licenceRegistrationDate, DateTime siteRegistrationDate);
        Driver GetDriver(string userId);
        CarViewModel GetDefaultCar(string driverId);
        List<CarViewModel> GetCars(string driverId);
        int AddOrUpdateCar(CarViewModel model, string userId);
    }
}
