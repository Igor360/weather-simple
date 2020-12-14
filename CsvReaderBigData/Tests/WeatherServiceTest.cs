using System;
using System.Collections.Generic;
using CsvReaderBigData.Models;
using CsvReaderBigData.Services;
using CsvReaderBigData.Servicestorss;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyCsvParser.Mapping;

namespace CsvReaderBigData.Tests
{
    [TestClass]
    public class WeatherServiceTest
    {
        [TestMethod]
        public static void test_time_execution()
        {
            WeatherService weatherService = new WeatherService();

            var watch = new System.Diagnostics.Stopwatch();
            String path =
                "/home/igor/KPI/BigDataCsvReader/CsvReaderBigData/CsvReaderBigData/Data/62318.30.11.2010.30.11.2020.1.0.0.en.utf8.00000000.csv";
            List<CsvMappingResult<WeatherDetails>> csvMappingResults = CsvReader.readFile(path);

            watch.Start();
            weatherService.calculeateMediumTemperature(csvMappingResults);
            watch.Stop();
            
            Console.WriteLine($"Execution time: {watch.ElapsedMilliseconds} ms");
        }
        
        public static void Main(string[] args)
        {
            test_time_execution();
        }
    }
}