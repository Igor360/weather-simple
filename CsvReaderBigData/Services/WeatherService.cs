using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvReaderBigData.Models;
using CsvReaderBigData.Services;
using TinyCsvParser.Mapping;

namespace CsvReaderBigData.Servicestorss
{
    public class WeatherService
    {
        public double CalculateMedium(IEnumerable<CsvMappingResult<WeatherDetails>> temperaturePerMonth)
        {
            var sum = temperaturePerMonth.ToList().Sum(n => n.Result.T);
            var count = temperaturePerMonth.ToList().Count;
            return sum / count;
        }

        public Dictionary<DateTime, List<CsvMappingResult<WeatherDetails>>> GroupByOneMonth(
            List<CsvMappingResult<WeatherDetails>> csvData)
        {
            var cultureInfo = new CultureInfo("en-US");
            var timeFormat = "dd.MM.yyyy HH:mm";
            var groupedData = new Dictionary<DateTime, List<CsvMappingResult<WeatherDetails>>>();

            return csvData
                .Where(x => x.Result?.localTime != null)
                .Select(x => new { Date = DateTime.ParseExact(x.Result.localTime, timeFormat, cultureInfo), Data = x })
                .GroupBy(x => $"{x.Date.Month}/{x.Date.Year}")
                .ToDictionary(x => x.First().Date, y => y.Select(el => el.Data).ToList());


            // DateTime firstDate;
            // while (csvData.Count != 0)
            // {
            //     firstDate = DateTime.ParseExact(csvData.Last().Result.localTime, timeFormat, cultureInfo);
            //     groupedData.Add(firstDate,
            //         csvData.Where(x =>
            //             x.Result != null &&
            //             (DateTime.ParseExact(x.Result.localTime, timeFormat, cultureInfo) - firstDate).Days <=
            //             30)
            //     );
            //     csvData = csvData.Where(x =>
            //             x.Result != null &&
            //             (DateTime.ParseExact(x.Result.localTime, timeFormat, cultureInfo) - firstDate).Days >
            //             30)
            //         .ToList();
            // }
            // return groupedData;
        }

        public Dictionary<DateTime, Double> CalculateMediumTemperature(Dictionary<DateTime, List<CsvMappingResult<WeatherDetails>>> groupedData)
        {
            var res = new Dictionary<DateTime, Double>();
            foreach (var entry in groupedData)
            {
                var mediumTemperature = this.CalculateMedium(entry.Value);
                res.Add(entry.Key, mediumTemperature);
            }

            return res;
        }

        public async Task<Dictionary<DateTime, Double>> CalculateMediumTemperatureByTasks(Dictionary<DateTime, List<CsvMappingResult<WeatherDetails>>> groupedData)
        {
            var res = new Dictionary<DateTime, Double>();

            var taskList = new List<Task<(DateTime, double)>>();
            foreach (var entry in groupedData)
            {
                taskList.Add(Task<(DateTime, double)>.Run(() =>
                {
                    return (entry.Key, Value: this.CalculateMedium(entry.Value));
                }));
            }

            foreach (var task in taskList)
            {
                var (Key, Value) = await task;
                res.Add(Key, Value);
            }

            return res;
        }

        public async Task<Dictionary<DateTime, Double>> CalculateMediumTemperatureByActors(Dictionary<DateTime, List<CsvMappingResult<WeatherDetails>>> groupedData, int actorsCount = 1)
        {
            var res = new Dictionary<DateTime, Double>();

            var calculators = new ActorPool(actorsCount);
            var taskList = new List<Task<(DateTime, double)>>();
            foreach (var entry in groupedData)
            {
                taskList.Add(calculators.GetActor().Enqueue(() =>
                {
                    return (entry.Key, Value: this.CalculateMedium(entry.Value));
                }));
            }
            await Task.WhenAll(taskList);

            foreach (var task in taskList)
            {
                var (Key, Value) = await task;
                res.Add(Key, Value);
            }

            return res;
        }
    }
}