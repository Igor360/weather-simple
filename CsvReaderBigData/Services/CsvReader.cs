using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvReaderBigData.Mappings;
using CsvReaderBigData.Models;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CsvReaderBigData.Services
{
    /**
     * @class CsvReader
     */
    public static class CsvReader
    {
        public static List<CsvMappingResult<WeatherDetails>> readFile(String path)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            WeatherMapping csvMapper = new WeatherMapping();
            CsvParser<WeatherDetails> csvParser = new CsvParser<WeatherDetails>(csvParserOptions, csvMapper);
            return csvParser
                .ReadFromFile($@"{path}", Encoding.ASCII)
                .ToList();
        }
    }
}