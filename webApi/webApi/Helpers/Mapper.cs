using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.ReviewDBO;
using webApi.DataTransferObjects.User;
using webApi.Models;

namespace webApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, NewUser>() // example of mapping
               .ReverseMap();

            CreateMap<DiscountCode, NewDiscountCode>() // example of mapping
                .ReverseMap();

            CreateMap<Review, NewReview>() // example of mapping
               .ReverseMap()
        }
    }
}