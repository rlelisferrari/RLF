using System;

namespace DOMAIN.Models
{
    public class HistoricalStockPrice
    {
        public DateTime? Date { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public double? Adjusted_close { get; set; }
        public double? Adjusted_low { get; }
        public double? Adjusted_high { get; }
        public double? Adjusted_open { get; }
        public long? Volume { get; set; }
    }
}
