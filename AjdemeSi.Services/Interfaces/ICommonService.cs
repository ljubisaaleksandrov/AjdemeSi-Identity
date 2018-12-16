using AjdemeSi.Domain.Models.Settings;
using System.Collections.Generic;

namespace AjdemeSi.Services.Interfaces
{
    public interface ICommonService
    {
        CarViewModel GetCarDetails(int carId);
        List<string> GetVehicleModels();
        List<string> GetVehicleMakes(string model);
    }
}
