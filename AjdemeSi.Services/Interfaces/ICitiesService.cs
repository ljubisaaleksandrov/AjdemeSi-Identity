using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Services.Interfaces
{
    public interface ICitiesService
    {
        List<string> GetCities(string term, int resoultsCount = 5);
    }
}
