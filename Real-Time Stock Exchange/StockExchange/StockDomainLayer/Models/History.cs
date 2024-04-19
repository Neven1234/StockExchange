using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDomainLayer.Models
{
    public class History
    {
        public int Id { get; set; }
        public ICollection< Order> Order { get; set; }
        public string userId { get; set; }
    }
}
