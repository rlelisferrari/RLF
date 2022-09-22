using System;
using System.ComponentModel.DataAnnotations;

namespace DOMAIN.Models
{
    public class CotacaoIntraDay
    {
        public CotacaoIntraDay(DateTime? dateTime, double? open, double? high, double? low, double? close, decimal? volume)
        {
            DateTime = (DateTime)dateTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DateTime { get; set; }

        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public decimal? Volume { get; set; }
        public double? LucroPrejuizo { get; set; }
    }
}
