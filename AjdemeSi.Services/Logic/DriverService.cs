using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AjdemeSi.Services.Logic
{
    public class DriverService : IDriverService
    {
        public readonly IMapper _mapper;
        private readonly ICommonService _commonService;
        public DriverService() { }
        public DriverService(IMapper mapper, ICommonService commonService)
        {
            _mapper = mapper;
            _commonService = commonService;
        }


        public string AddDriver(string userId, DateTime licenceRegistrationDate, DateTime siteRegistrationDate)
        {
            using (DataContext db = new DataContext())
            {
                db.Drivers.Add(new Driver()
                {
                    UserId = userId,
                    LicenceRegistrationDate = licenceRegistrationDate,
                    SiteRegistrationDate = siteRegistrationDate
                });
                db.SaveChanges();
                
                return userId;
            }
        }

        public Driver GetDriver(string userId)
        {
            using (DataContext db = new DataContext())
            {
                return db.Drivers.FirstOrDefault(u => u.UserId == userId);
            }
        }

        public CarViewModel GetDefaultCar(string driverId)
        {
            using (DataContext db = new DataContext())
            {
                var car = db.Cars.FirstOrDefault(c => c.UserId == driverId && c.IsDefault);
                return car != null ? _mapper.Map<CarViewModel>(car) : null;
            }
        }

        public List<CarViewModel> GetCars(string driverId)
        {
            List<CarViewModel> driversCars = new List<CarViewModel>();
            using (DataContext db = new DataContext())
            {
                var vehicleModels = _commonService.GetVehicleModels();
                var cars = db.Cars.Where(c => c.UserId == driverId).ToList();
                foreach(var car in cars)
                {
                    var driversCar = _mapper.Map<CarViewModel>(cars);
                    driversCar.VehiclesViewModel = vehicleModels;
                    driversCars.Add(driversCar);
                }
            }
            return driversCars;
        }

        public int AddOrUpdateCar(CarViewModel model, string userId)
        {
            using (DataContext db = new DataContext())
            {
                Car car;
                if (model.Id != 0)
                {
                    car = db.Cars.FirstOrDefault(c => c.Id == model.Id);
                    car.Make = model.Make;
                    car.Model = model.Model;
                    car.NumberOfSits = model.NumberOfSits;
                    car.YearOfManufacture = model.YearOfManufacture;
                }
                else
                {
                    car = _mapper.Map<Car>(model);
                    car.UserId = userId;
                    car.IsDefault = GetDefaultCar(userId) == null;
                    db.Cars.Add(car);
                }

                db.SaveChanges();

                return car.Id;
            }
        }
    }
}
