using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using webApi.DataTransferObjects.AddressDTO;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.SectionDTO;
using webApi.DataTransferObjects.UserDTO;
using webApi.Enums;
using webApi.Models;

namespace webApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User Mappers
            CreateMap<Models.User, NewCustomer>().ReverseMap();

            //Discount Code Mappers
            CreateMap<DiscountCode, NewDiscountCode>().ReverseMap();

            CreateMap<DiscountCode, DiscountCodeDTO>().ReverseMap();

            //Review Mappers
            CreateMap<Review, NewReview>().ReverseMap();

            CreateMap<Review, ReviewDTO>().ReverseMap();

            //Order Mapper
            CreateMap<Order, OrderDTO>() //TODO(?): mapping for enums
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => new AddressDTO() { Street = src.Address.Street, City = src.Address.City, PostCode = src.Address.PostCode }));

            CreateMap<NewOrder, Order>()
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => null as object));

            //Complaint Mapper
            CreateMap<Complaint, ComplaintDTO>().ReverseMap();

            CreateMap<Complaint, NewComplaint>().ReverseMap();

            //Restaurant Mappers
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => (RestaurantState)src.State))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => new AddressDTO() { Street = src.Address.Street, City = src.Address.City, PostCode = src.Address.PostCode }))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => ((RestaurantState)src.State).ToString("G")));

            CreateMap<NewRestaurant, Restaurant>()
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => null as object))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => 1));

            CreateMap<AddressDTO, Address>();

            CreateMap<Section, SectionDTO>();

            CreateMap<Dish, NewPositionFromMenu>().ReverseMap();

            CreateMap<Dish, PositionFromMenuDTO>().ReverseMap();
        }
    }
}