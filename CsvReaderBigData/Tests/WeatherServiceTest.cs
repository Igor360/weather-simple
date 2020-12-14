using System;
using CsvReaderBigData.Services;
using CsvReaderBigData.Servicestorss;

namespace CsvReaderBigData.Tests
{
    public class WeatherServiceTest
    {
        public static void TestTimeExecution()
        {
            var weatherService = new WeatherService();

            var path = "Data/62318.30.11.2010.30.11.2020.1.0.0.en.utf8.00000000.csv";

            var csvMappingResults = weatherService.GroupByOneMonth(CsvReader.readFile(path));

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            weatherService.CalculateMediumTemperature(csvMappingResults);
            watch.Stop();
            Console.WriteLine($"Simple; Execution time: {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            weatherService.CalculateMediumTemperatureByTasks(csvMappingResults).GetAwaiter().GetResult();
            watch.Stop();
            Console.WriteLine($"Tasks (ThreadPool); Execution time: {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            weatherService.CalculateMediumTemperatureByActors(csvMappingResults).GetAwaiter().GetResult();
            watch.Stop();
            Console.WriteLine($"Actors (1); Execution time: {watch.ElapsedMilliseconds} ms");


            watch.Restart();
            weatherService.CalculateMediumTemperatureByActors(csvMappingResults, 3).GetAwaiter().GetResult();
            watch.Stop();
            Console.WriteLine($"Actors (3); Execution time: {watch.ElapsedMilliseconds} ms");
        }
    }
}