using AutoMapper;
using StockDomainLayer.Models;

namespace StockExchange.DTOs
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserToReturnDTO>().ReverseMap();
        }
    }
}
