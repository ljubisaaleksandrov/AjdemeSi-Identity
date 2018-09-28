using AjdemeSi.Domain.Models.Settings;

namespace AjdemeSi.Domain.Models.Ride
{
    public class RideDashboardViewModel
    {
        public RidePagedListViewModel SuggestedRidesListViewModel { get; set; }
        public RidePagedListViewModel MyRidesListViewModel { get; set; }
        public UserResultsSettingsExtendedViewModel UserCommonViewModel { get; set; }
        public CarViewModel CarDetails { get; set; }
    }
}
