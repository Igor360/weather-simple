using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvReaderBigData.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CsvReaderBigData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WeatherServiceTest.TestTimeExecution();
        }
    }
}