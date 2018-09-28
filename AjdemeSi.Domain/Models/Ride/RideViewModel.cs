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
        public string DriverUserId { get; set; }
        public string FromCounty { get; set; }
        public string FromCity { get; set; }
        public string ToCounty { get; set; }
        public string ToCity { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int TravelTime { get; set; }
        public int MaxPassengers { get; set; }
        public float TotalPrice { get; set; }
        public float MinTotalPrice { get; set; }
        public float PricePerPassenger { get; set; }
        public Nullable<int> ReturnDrivingId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> DelayedStartTime { get; set; }
        public Nullable<int> BreakTime { get; set; }

        public RideDriverViewModel DriverDetails { get; set; }
        public List<RidePassengerViewModel> Passengers { get; set; }

        public RideViewModel()
        {
            DriverDetails = new RideDriverViewModel();
            Passengers = new List<RidePassengerViewModel>();
        }
    }
}