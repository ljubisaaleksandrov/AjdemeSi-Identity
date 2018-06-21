using AutoMapper;
using PagedList;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using RideEF = AjdemeSi.Domain.Ride;

namespace AjdemeSi.Domain.Models.Ride
{
    public class RidePagedListViewModel : PagedList<RideEF>
    {
        public IEnumerable<RideViewModel> ListEntries { get; set; }
        public DateTime DateFrom;
        public DateTime DateTo;
        public string CountryFrom;
        public string CityFrom;
        public string CountryTo;
        public string CityTo;
        public List<SelectListItem> CountsList { get; set; }

        public RidePagedListViewModel(IQueryable<RideEF> superset,
                                      int pageNumber, 
                                      int pageSize, 
                                      int totalItemsCount,
                                      IMapper mapper,
                                      string countryFrom = "",
                                      string cityFrom = "",
                                      string countryTo = "",
                                      string cityTo = "")
        : base(superset, pageNumber, pageSize)
        {
            ListEntries = mapper.Map<List<RideViewModel>>(superset.ToList());
            TotalItemCount = totalItemsCount;
            PageCount = totalItemsCount % pageSize == 0 ? totalItemsCount / pageSize : totalItemsCount / pageSize + 1;
            IsLastPage = PageCount == pageNumber;
            HasNextPage = !IsLastPage;
            HasPreviousPage = pageNumber != 1;

            CountryFrom = countryFrom;
            CityFrom = cityTo;
            CountryTo = countryTo;
            CityTo = cityTo;

            CountsList = new List<SelectListItem>()
                               {
                                   new SelectListItem() { Selected = PageCount == 10, Text = "10", Value = "10" },
                                   new SelectListItem() { Selected = PageCount == 20, Text = "20", Value = "20" },
                                   new SelectListItem() { Selected = PageCount == 50, Text = "50", Value = "50" },
                                   new SelectListItem() { Selected = PageCount == 100, Text = "100", Value = "100" }
                               };
        }

         public RidePagedListViewModel(IEnumerable<RideEF> superset, 
                                              int pageNumber, 
                                              int pageSize, 
                                              int totalItemsCount,
                                              IMapper mapper,
                                              string countryFrom = "",
                                              string cityFrom = "",
                                              string countryTo = "",
                                              string cityTo = "")
        : this(superset.AsQueryable<RideEF>(), pageNumber, pageSize, totalItemsCount, mapper, countryFrom, cityFrom, countryTo, cityTo)
        { }
    }
}
