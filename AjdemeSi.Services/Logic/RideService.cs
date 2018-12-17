using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

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

        public RideCreationViewModel CreateRide(string placeFrom, string placeTo, string dateFrom, string timeFrom, string timeDelay, string timeTravel, string timeBreak, string passengersNomber, bool isTotalPrice, float price, string userId)
        {
            RideCreationViewModel viewModel = new RideCreationViewModel();

            try
            {
                using (DataContext db = new DataContext())
                {
                    string[] placeFromArgs = placeFrom.Split(new string[] { " (" }, StringSplitOptions.None).Select(p => p.TrimEnd(new char[] { ')' })).ToArray();
                    string[] placeToArgs = placeTo.Split(new string[] { " (" }, StringSplitOptions.None).Select(p => p.TrimEnd(new char[] { ')' })).ToArray();

                    DateTime startTime = Convert.ToDateTime(dateFrom + " " + timeFrom);
                    int travelTimeMinutes = Convert.ToInt32(Math.Floor(new TimeSpan(Convert.ToDateTime(timeTravel).TimeOfDay.Ticks).TotalMinutes));
                    int delayTimeMinutes = !String.IsNullOrEmpty(timeDelay) ? Convert.ToInt32(Math.Floor(new TimeSpan(Convert.ToDateTime(timeDelay).TimeOfDay.Ticks).TotalMinutes)) : 0;
                    int breakTimeMinutes = !String.IsNullOrEmpty(timeBreak) ? Convert.ToInt32(Math.Floor(new TimeSpan(Convert.ToDateTime(timeBreak).TimeOfDay.Ticks).TotalMinutes)) : 0;

                    db.Rides.Add(new Ride()
                    {
                        FromCity = placeFromArgs[0],
                        FromCounty = placeFromArgs[1],
                        ToCity = placeToArgs[0],
                        ToCounty = placeToArgs[1],
                        StartTime = startTime,
                        DriverUserId = userId,
                        TravelTime = travelTimeMinutes,
                        DelayedStartTime = delayTimeMinutes,
                        BreakTime = breakTimeMinutes,
                        MaxPassengers = Int32.Parse(passengersNomber),
                        TotalPrice = isTotalPrice ? price : 0,
                        PricePerPassenger = !isTotalPrice ? price : 0,
                        EndTime = startTime.AddMinutes(travelTimeMinutes + delayTimeMinutes + breakTimeMinutes),
                        DateCreated = DateTime.Now
                    });

                    db.SaveChanges();
                }
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);  // todo
                    }
                }
            }
            catch(Exception e)
            {
                // todo
            }
            return viewModel;
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

                rides = rides.Where(r => r.DriverUserId == driverId && r.StartTime >= DateTime.Now).OrderBy(r => r.StartTime);
                int totalCount = rides.Count();

                return new RidePagedListViewModel(rides, pageIndex, countOnPage, totalCount, _mapper, String.Empty, String.Empty, String.Empty, String.Empty);
            }
        }

        public RidePagedListViewModel GetDriverSuggestedRides(DateTime? dateFrom, DateTime? dateTo, string countryFrom, string cityFrom, string countryTo, string cityTo, string sortOrder = null, int pageIndex = 1, int countOnPage = 10)
        {
            using (DataContext db = new DataContext())
            {
                IQueryable<Ride> rides = db.Rides;

                rides = rides.Where(r => r.StartTime >= DateTime.Now);
                int totalCount = rides.Count();

                return new RidePagedListViewModel(rides, pageIndex, countOnPage, totalCount, _mapper, String.Empty, String.Empty, String.Empty, String.Empty);
            }
        }
    }
}