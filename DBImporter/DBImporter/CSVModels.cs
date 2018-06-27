using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace DBImporter
{
    public class CityModel
    {
        public string City { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
    }

    public class CsvPersonMapping : CsvMapping<CityModel>
    {

        public CsvPersonMapping()
            : base()
        {
            //  Country,City,AccentCity,Region,Population,Latitude,Longitude
            MapProperty(0, x => x.Country);
            MapProperty(2, x => x.City);
            MapProperty(4, x => x.Population);
        }
    }
}
