using System.Collections.Generic;
using AjdemeSi.Services.Interfaces;
using AjdemeSi.Domain;
using System.Linq;

namespace AjdemeSi.Services.Logic
{
    public class CitiesService : ICitiesService
    {
        public List<string> GetCities(string term, int resoultsCount = 5)
        {
            using(DataContext dx = new DataContext())
            {
                return dx.Cities.Where(c => c.Name.Contains(term) || c.NameAlternative.Contains(term))
                                .Take(resoultsCount)
                                .Select(c => c.Name + " (" + c.Country + ")")
                                .ToList<string>();
            }
        }
    }
}
