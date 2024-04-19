using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDomainLayer.Models
{
    public class Stock
    {
        public class data
        {
            public string symbol { get; set; }
            public string currency { get; set; }
            public double regularMarketPrice { get; set; }
            public double fiftyTwoWeekHigh { get; set; }
            public double fiftyTwoWeekLow { get; set; }
            public double regularMarketDayHigh { get; set; }
            public double regularMarketDayLow { get; set; }
            public double regularMarketVolume { get; set; }
            public double chartPreviousClose { get; set; }
            public int[] timestamp { get; set; }
        }

        public class root
        {
            public data meta { get; set; }
            public int[] timestamp { get; set; }
            public int status { get; set; }
        }
    }
}
