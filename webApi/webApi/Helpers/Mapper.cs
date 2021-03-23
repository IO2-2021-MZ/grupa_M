using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using webApi.DataTransferObjects.User;
using webApi.Models;

namespace webApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.User, NewCustomer>() // example of mapping
               .ReverseMap();
        }
    }
}