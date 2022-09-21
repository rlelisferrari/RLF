using System;

namespace DOMAIN.Models
{
    public class CotacaoIntraDay
    {
        public CotacaoIntraDay(DateTime? dateTime, double? open, double? high, double? low, double? close)
        {
            DateTime = dateTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
        }

        public DateTime? DateTime { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public decimal? Volume { get; set; }
        public double? LucroPrejuizo { get; set; }
    }
}
