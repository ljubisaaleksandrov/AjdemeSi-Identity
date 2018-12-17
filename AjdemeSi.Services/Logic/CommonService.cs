﻿using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Identity;
using AjdemeSi.Domain.Models.Settings;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AjdemeSi.Services.Logic
{
    public class CommonService : ICommonService
    {
        private readonly IMapper _mapper;

        public CommonService() { }

        public CommonService(IMapper mapper, IDriverService driverService)
        {
            _mapper = mapper;
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

        public List<string> GetVehicleModels(string make)
        {
            using (DataContext db = new DataContext())
            {
                var vehicles = db.Vehicles.Where(v => v.Make == make).GroupBy(v => v.Model);
                return vehicles.Select(vg => vg.Key).OrderBy(vgk => vgk).ToList();
            }
        }

        public List<string> GetVehicleMakes()
        {
            using (DataContext db = new DataContext())
            {
                var vehicles = db.Vehicles.GroupBy(v => v.Make);
                return vehicles.Select(vg => vg.Key).OrderBy(vgk => vgk).ToList();
            }
        }
        #endregion
    }
}
