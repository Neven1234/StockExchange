using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDomainLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string SampleStockName { get; set; }
        public int Quantity { get; set; }
        public double regularMarketPrice { get; set; }
        public string OrderType { get; set; }
        public string UserId { get; set; }
    }
}
