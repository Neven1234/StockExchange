using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDomainLayer.Models
{
    public class User: IdentityUser
    {
        public ICollection<Order> orders {  get; set; }
    }
}
