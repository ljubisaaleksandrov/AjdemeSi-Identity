using AjdemeSi.Domain.Models.Ride;

namespace AjdemeSi.Services.Interfaces
{
    public interface ISiteLabelsService
    {
        RideSiteLabelsViewModel GetRideSiteLabels(string language);
    }
}
