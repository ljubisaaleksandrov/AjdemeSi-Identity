using System.Collections.Generic;

namespace AjdemeSi.Domain.Models.Settings
{
    public class SettingsViewModel
    {
        public UserGeneralViewModel UserGeneralViewModel { get; set; }
        public List<CarViewModel> CarsViewModel { get; set; }
        public UserResultsSettingsViewModel UserResultsViewModel { get; set; }
        public List<string> VehiclesViewModel { get; set; }

        public SettingsViewModel() { }
    }
}
