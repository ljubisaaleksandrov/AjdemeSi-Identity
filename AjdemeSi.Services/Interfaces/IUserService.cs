using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Settings;
using System.Collections.Generic;

namespace AjdemeSi.Services.Interfaces
{
    public interface IUserService
    {
        UserGeneralViewModel GetUserGeneralDetails(string userId);
        UserResultsSettingsViewModel GetUserResultsSettingsViewModel(string userId);
        AspNetUser GetUser(string userId);
    }
}
