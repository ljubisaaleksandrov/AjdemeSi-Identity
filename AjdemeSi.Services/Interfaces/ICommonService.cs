using AjdemeSi.Domain.Models.Settings;

namespace AjdemeSi.Services.Interfaces
{
    public interface ICommonService
    {
        CarViewModel GetCarDetails(int carId);
    }
}
