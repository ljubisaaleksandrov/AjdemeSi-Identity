using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AjdemeSi.Domain.Models.Ride
{
    public class RideViewModel
    {
        //this.Ride1 = new HashSet<Ride>();
        //this.RidePassangers = new HashSet<RidePassanger>();
        //this.UsersGroups = new HashSet<UsersGroup>();

        public int Id { get; set; }
        public string FromCounty { get; set; }
        public string FromCity { get; set; }
        public string FromPlace { get; set; }
        public string ToCounty { get; set; }
        public string ToCity { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TravelTime { get; set; }
        public int MaxPassengers { get; set; }
        public float TotalPrice { get; set; }
        public float MinTotalPrice { get; set; }
        public float PricePerPassenger { get; set; }
        public int? ReturnDrivingId { get; set; }
        public DateTime? DateCreated { get; set; }

        public RideDriverViewModel Driver { get; set; }
        public List<RidePassengerViewModel> Passengers { get; set; }

        public RideViewModel()
        {
            Driver = new RideDriverViewModel();
            Passengers = new List<RidePassengerViewModel>();
        }
    }
}