using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Identity;
using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AjdemeSi.Services.Logic
{
    public class CommonService : ICommonService
    {
        private readonly IMapper _mapper;
        private readonly IDriverService _driverService;

        public CommonService() { }

        public CommonService(IMapper mapper, IDriverService driverService)
        {
            _mapper = mapper;
            _driverService = driverService;
        }

        #region Car Methods
        public CarViewModel GetCarDetails(int carId)
        {
            using (DataContext db = new DataContext())
            {
                var car = db.Cars.FirstOrDefault(c => c.Id == carId);
                return car != null ? _mapper.Map<CarViewModel>(car) : null;
            }
        }
        #endregion
    }
}
