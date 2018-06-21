using AjdemeSi.Domain.Models.Common;

namespace AjdemeSi.Domain.Models.Ride
{
    public class RideDashboardViewModel
    {
        public RidePagedListViewModel RidePagedListViewModel { get; set; }
        public RidePagedListViewModel DriverRidePagedListViewModel { get; set; }
        public UserCommonViewModel UserCommonViewModel { get; set; }
    }
}
