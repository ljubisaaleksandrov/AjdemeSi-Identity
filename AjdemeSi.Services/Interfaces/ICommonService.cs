using AjdemeSi.Domain.Models.Settings;
using System.Collections.Generic;

namespace AjdemeSi.Services.Interfaces
{
    public interface ICommonService
    {
        CarViewModel GetCarDetails(int carId);
        List<string> GetVehicleMakes();
        List<string> GetVehicleModels(string make);
    }
}
