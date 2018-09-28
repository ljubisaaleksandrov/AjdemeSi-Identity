using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace AjdemeSi.Services.Logic
{
    public class UserService : IUserService
    {
        public readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public AspNetUser GetUser(string userId)
        {
            using (DataContext db = new DataContext())
            {
                return db.AspNetUsers.FirstOrDefault(u => u.Id == userId);
            }
        }

        public UserGeneralViewModel GetUserGeneralDetails(string userId)
        {
            using (DataContext db = new DataContext())
            {
                var userModel = _mapper.Map<UserGeneralViewModel>(GetUser(userId));
                return (UserGeneralViewModel)userModel;
            }
        }

        public UserResultsSettingsViewModel GetUserResultsSettingsViewModel(string userId)
        {
            return new UserResultsSettingsViewModel(); // todo
        }
    }
}
