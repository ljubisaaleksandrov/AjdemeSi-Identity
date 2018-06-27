using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace DBImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvPersonMapping csvMapper = new CsvPersonMapping();
            CsvParser<CityModel> csvParser = new CsvParser<CityModel>(csvParserOptions, csvMapper);

            var result = csvParser
                .ReadFromFile(@"C:\Users\lyubisha aleksandrov\Documents\Visual Studio 2017\Projects\DBImporter\worldcitiespop.txt", Encoding.ASCII)
                .Where(x => x.Error == null)
                .ToList();

            var RScities = result.Select(x => x.Result).Where(c => !String.IsNullOrEmpty(c.Country) && c.Country == "rs");
            var RSbigCities = RScities.Where(x => x.Population > 0).OrderBy(x => x.Population).ToList();

            var BGcities = result.Select(x => x.Result).Where(c => !String.IsNullOrEmpty(c.Country) && c.Country == "bg");
            var BGbigCities = BGcities.Where(x => x.Population > 0).OrderBy(x => x.Population).ToList();

            using(Default_ASEntities dx = new Default_ASEntities())
            {
                foreach (var cityModel in RScities)
                {
                    if (!dx.Cities.Any(c => c.Name == cityModel.City && c.Country == cityModel.Country))
                    {
                        dx.Cities.Add(new City()
                        {
                            Name = cityModel.City,
                            Country = "Serbia"
                        });
                    }
                }
                foreach (var cityModel in BGcities)
                {
                    if (!dx.Cities.Any(c => c.Name == cityModel.City && c.Country == cityModel.Country))
                    {
                        dx.Cities.Add(new City()
                        {
                            Name = cityModel.City,
                            Country = "Bulgaria"
                        });
                    }
                }
                dx.SaveChanges();
            }

            Console.ReadLine();
        }
    }
}
