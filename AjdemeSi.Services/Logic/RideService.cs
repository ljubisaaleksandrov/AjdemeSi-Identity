using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Identity;
using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Services.Interfaces.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AjdemeSi.Services.Logic
{
    public class RideService : IRideService
    {
        private readonly IMapper _mapper;

        public RideService() {}

        public RideService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public RidePagedListViewModel GetAll(DateTime? dateFrom, DateTime? dateTo, string countryFrom, string cityFrom, string countryTo, string cityTo, string sortOrder = null, int pageIndex = 1, int countOnPage = 10)
        {
            using (DataContext db = new DataContext())
            {
                IQueryable<Ride> rides = db.Rides;

                rides = rides.Where(r => r.FromCounty.Equals(countryFrom) &&
                                         r.FromCity.Equals(cityTo) &&
                                         r.ToCounty.Equals(countryTo) &&
                                         r.ToCity.Equals(cityTo));

                rides = rides.Where(u => u.StartTime >= dateFrom && u.StartTime <= dateTo);

                switch (sortOrder)
                {
                    //case "registrationDate_asc":
                    //    rides = rides.OrderBy(x => x.DateCreated);
                    //    break;
                    //case "username_asc":
                    //    rides = rides.OrderBy(x => x.UserName);
                    //    break;
                    //case "username_desc":
                    //    rides = rides.OrderByDescending(x => x.UserName);
                    //    break;
                    //case "email_asc":
                    //    rides = rides.OrderBy(x => x.Email);
                    //    break;
                    //case "email_desc":
                    //    rides = rides.OrderByDescending(x => x.Email);
                    //    break;
                    //case "isConfirmed_asc":
                    //    rides = rides.OrderBy(x => x.EmailConfirmed);
                    //    break;
                    //case "isConfirmed_desc":
                    //    rides = rides.OrderByDescending(x => x.EmailConfirmed);
                    //    break;
                    default:
                        rides = rides.OrderByDescending(x => x.StartTime);
                        break;
                }

                int totalCount = rides.Count();

                rides = rides.Skip(countOnPage * (pageIndex - 1)).Take(countOnPage);
                return new RidePagedListViewModel(rides, pageIndex, countOnPage, totalCount, _mapper, countryFrom, cityFrom, countryTo, cityTo);
            }
        }

        public RidePagedListViewModel GetDriverActiveRides(string driverId, string sortOrder = null, int pageIndex = 1, int countOnPage = 10)
        {
            using (DataContext db = new DataContext())
            {
                IQueryable<Ride> rides = db.Rides;

                rides = rides.Where(r => r.DriverUserId == driverId && r.StartTime >= DateTime.Now);
                int totalCount = rides.Count();

                return new RidePagedListViewModel(rides, pageIndex, countOnPage, totalCount, _mapper, String.Empty, String.Empty, String.Empty, String.Empty);
            }

        }
    }
}