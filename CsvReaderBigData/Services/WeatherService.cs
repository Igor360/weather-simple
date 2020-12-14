using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CsvReaderBigData.Models;
using TinyCsvParser.Mapping;

namespace CsvReaderBigData.Servicestorss
{
    public class WeatherService
    {
        public double calculateMediumTemperature(IEnumerable<CsvMappingResult<WeatherDetails>> temperaturePerMonth)
        {
            Double sum = temperaturePerMonth.ToList().Sum(n => n.Result.T);
            int count = temperaturePerMonth.ToList().Count;
            return sum / count;
        }

        public Dictionary<DateTime, IEnumerable> groupByOneMonth(
            List<CsvMappingResult<WeatherDetails>> csvData)
        {
            var cultureInfo = new CultureInfo("en-US");
            var timeFormat = "dd.MM.yyyy HH:mm";
            DateTime firstDate;
            Dictionary<DateTime, IEnumerable> groupedData =
                new Dictionary<DateTime, IEnumerable>();
            while (csvData.Count != 0)
            {
                firstDate = DateTime.ParseExact(csvData.Last().Result.localTime, timeFormat, cultureInfo);
                groupedData.Add(firstDate,
                    csvData.Where(x =>
                        x.Result != null &&
                        (DateTime.ParseExact(x.Result.localTime, timeFormat, cultureInfo) - firstDate).Days <=
                        30)
                );
                csvData = csvData.Where(x =>
                        x.Result != null &&
                        (DateTime.ParseExact(x.Result.localTime, timeFormat, cultureInfo) - firstDate).Days >
                        30)
                    .ToList();
            }

            return groupedData;
        }

        public Dictionary<DateTime, Double> calculeateMediumTemperature(List<CsvMappingResult<WeatherDetails>> csvData)
        {
            Dictionary<DateTime, IEnumerable> groupedData = this.groupByOneMonth(csvData);
            Dictionary<DateTime, Double> res = new Dictionary<DateTime, Double>();
            Double mediumTemperature;
            foreach (KeyValuePair<DateTime, IEnumerable> entry in groupedData)
            {
                mediumTemperature =
                    this.calculateMediumTemperature((IEnumerable<CsvMappingResult<WeatherDetails>>) entry.Value);
                res.Add(entry.Key, mediumTemperature);
            }

            return res;
        }
    }
}