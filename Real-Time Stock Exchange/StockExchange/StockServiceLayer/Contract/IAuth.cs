using StockDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServiceLayer.Contract
{
    public interface IAuth
    {
        Task<string> Register(string username, string password,string email);
        Task<User> Login(string username, string password);

    }
}
