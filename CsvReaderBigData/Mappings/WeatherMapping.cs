using CsvReaderBigData.Models;
using TinyCsvParser.Mapping;

namespace CsvReaderBigData.Mappings
{
    public class WeatherMapping : CsvMapping<WeatherDetails>
    {
        
        public WeatherMapping()
            : base()
        {
            MapProperty(0, x => x.localTime);
            MapProperty(1, x => x.T);
            MapProperty(2, x => x.Po);
            MapProperty(3, x => x.P);
            MapProperty(4, x => x.Pa);
            MapProperty(5, x => x.U);
            MapProperty(6, x => x.DD);
            MapProperty(7, x => x.Ff);
            MapProperty(8, x => x.ff10);
            MapProperty(9, x => x.ff3);
            MapProperty(10, x => x.N);
            MapProperty(11, x => x.WW);
            MapProperty(12, x => x.W1);
            MapProperty(13, x => x.W2);
            MapProperty(14, x => x.Tn);
            MapProperty(15, x => x.Tx);
            MapProperty(16, x => x.Cl);
            MapProperty(17, x => x.Nh);
            MapProperty(18, x => x.H);
            MapProperty(19, x => x.Cm);
            MapProperty(20, x => x.Ch);
            MapProperty(21, x => x.VV);
            MapProperty(22, x => x.Td);
            MapProperty(23, x => x.RRR);
            MapProperty(24, x => x.tR);
            MapProperty(25, x => x.E);
            MapProperty(26, x => x.Tg);
            MapProperty(27, x => x.e);
            MapProperty(28, x => x.sssl);
        }
    }    
}