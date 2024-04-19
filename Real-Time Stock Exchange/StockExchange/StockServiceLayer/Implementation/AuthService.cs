using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockDomainLayer.Models;
using StockServiceLayer.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockServiceLayer.Implementation
{
    public class AuthService : IAuth
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager,IConfiguration configuration)
        {
           _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                
                return user;
            }
            return null;
        }

        public async Task<string> Register(string username, string password, string email)
        {
            var userExist=await _userManager.FindByNameAsync(username);
            if(userExist != null)
            {
                return "user already exist";
            }
            User newUser = new User
            {
                UserName = username,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),

            };
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                return result.Errors.First().Description;
            }
            return "user created successgully";

        }

       
    }
}
