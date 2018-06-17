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

        public RidePagedListViewModel GetAll(DateTime dateFrom, DateTime dateTo, string countryFrom, string cityFrom, string countryTo, string cityTo, string sortOrder = null, int pageIndex = 1, int countOnPage = 10)
        {
            using (DataContext db = new DataContext())
            {
                IQueryable<Ride> rides = db.Rides;

                rides = rides.Where(r => r.FromCounty.Equals(countryFrom) &&
                                         r.FromCity.Equals(cityTo) &&
                                         r.ToCounty.Equals(countryTo) &&
                                         r.ToCity.Equals(cityTo));

                rides = rides.Where(u => u.DateCreated >= dateFrom && u.DateCreated <= dateTo);

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
                    //default:
                    //    rides = rides.OrderByDescending(x => x.DateCreated);
                    //    break;
                }

                int totalCount = rides.Count();

                rides = rides.Skip(countOnPage * (pageIndex - 1)).Take(countOnPage);
                return new RidePagedListViewModel(rides, pageIndex, countOnPage, totalCount, countryFrom, cityFrom, countryTo, cityTo, _mapper);
            }
        }

        public AspNetUser GetUser(string id)
        {
            using (DataContext dc = new DataContext())
            {
                return dc.AspNetUsers.FirstOrDefault(u => u.Id == id);
            }
        }

        public bool UpdateUser(AspNetUser user)
        {
            using (DataContext dc = new DataContext())
            {
                var currentUser = dc.AspNetUsers.FirstOrDefault(u => u.Id == user.Id);
                if(currentUser != null)
                {
                    currentUser.UserName = user.UserName;
                    currentUser.PhoneNumber = user.PhoneNumber;
                    dc.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool RemoveUser(string id)
        {
            using (DataContext dc = new DataContext())
            {
                var currentUser = dc.AspNetUsers.FirstOrDefault(u => u.Id == id);
                if (currentUser != null)
                {
                    dc.AspNetUsers.Remove(currentUser);
                    dc.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool ContactExists(string id, string userName, string email)
        {
            using (DataContext dc = new DataContext())
            {
                return (!String.IsNullOrEmpty(id) && dc.AspNetUsers.Any(u => u.Id == id)) ||
                       (!String.IsNullOrEmpty(userName) && dc.AspNetUsers.Any(u => u.UserName == userName)) ||
                       (!String.IsNullOrEmpty(email) && dc.AspNetUsers.Any(u => u.Email == email)); 
            }
        }

        public bool ConfirmEmail(string id, string email)
        {
            using (DataContext dc = new DataContext())
            {
                var currentUser = dc.AspNetUsers.FirstOrDefault(u => u.Id == id);
                if (currentUser != null)
                    currentUser = dc.AspNetUsers.FirstOrDefault(u => u.Email == email);
                if (currentUser != null)
                {
                    currentUser.EmailConfirmed = true;
                    dc.SaveChanges();
                    return true;
                }

                return false;
            }
        }


        public bool BlockUser(string id, string email)
        {
            using (DataContext dc = new DataContext())
            {
                var currentUser = dc.AspNetUsers.FirstOrDefault(u => u.Id == id);
                if (currentUser != null)
                    currentUser = dc.AspNetUsers.FirstOrDefault(u => u.Email == email);
                if (currentUser != null)
                {
                    currentUser.EmailConfirmed = true;
                    dc.SaveChanges();
                    return false;
                }

                return false;
            }
        }

    }
}