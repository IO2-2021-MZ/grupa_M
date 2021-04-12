using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.UserDTO;
using webApi.Models;

namespace webApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.User, NewCustomer>() // example of mapping
               .ReverseMap();

            CreateMap<DiscountCode, NewDiscountCode>() // example of mapping
                .ReverseMap();

            CreateMap<Review, NewReview>() // example of mapping
               .ReverseMap();

            CreateMap<Complaint, NewComplaint>()
               .ReverseMap();
        }
    }
}