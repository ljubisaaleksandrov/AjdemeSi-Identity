    namespace AjdemeSi.Domain.Models.Settings
{
    public class UserResultsSettingsViewModel
    {

        /// <summary>
        /// Load other ride results if the user is driver - false by default
        /// </summary>
        public bool LoadResults { get; set; }

        //todo
        public UserResultsSettingsViewModel()
        {
            LoadResults = true;
        }
    }

    public class UserResultsSettingsExtendedViewModel : UserResultsSettingsViewModel
    {
        public bool IsDriver { get; set; }

        public UserResultsSettingsExtendedViewModel() { }

        public UserResultsSettingsExtendedViewModel(bool isDriver)
        {
            IsDriver = isDriver;
        }
    }
}
