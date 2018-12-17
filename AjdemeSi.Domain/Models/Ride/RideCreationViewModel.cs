using System;
using System.Collections.Generic;

namespace AjdemeSi.Domain.Models.Ride
{
    public class RideCreationViewModel
    {
        //this.Ride1 = new HashSet<Ride>();
        //this.RidePassangers = new HashSet<RidePassanger>();
        //this.UsersGroups = new HashSet<UsersGroup>();

        public int Id { get; set; }
        public string DriverUserId { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime StartTime { get; set; }
        public TimeSpan TravelTime { get; set; }
        public int MaxPassengers { get; set; }
        public string SelectedPriceType { get; set; }
        public float Price { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public TimeSpan DelayedStartTime { get; set; }
        public TimeSpan BreakTime { get; set; }
        public string AdditionalInfo { get; set; }


        public Nullable<int> ReturnRideId { get; set; }
        public System.DateTime ReturnStartDate { get; set; }
        public TimeSpan ReturnStartTime { get; set; }
        public TimeSpan ReturnTravelTime { get; set; }
        public int ReturnMaxPassengers { get; set; }
        public float ReturnSelectedPriceType { get; set; }
        public float ReturnPrice { get; set; }
        public TimeSpan ReturnDelayedStartTime { get; set; }
        public TimeSpan ReturnBreakTime { get; set; }
        public string ReturnAdditionalInfo { get; set; }


        public RideDriverViewModel DriverDetails { get; set; }
        //public List<RidePassengerViewModel> Passengers { get; set; }

        public RideCreationViewModel()
        {
            DriverDetails = new RideDriverViewModel();
            //Passengers = new List<RidePassengerViewModel>();
        }
    }
}