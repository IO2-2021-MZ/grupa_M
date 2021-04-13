using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class OrderA
    {
        public int Id { get; set; }
        public int PaymentMethod { get; set; }
        public int State { get; set; }
        public DateTime Date { get; set; }
        public AddressDTO.AddressDTO Address { get; set; }
        public DiscountCodeDTO.DiscountCodeDTO DiscountCode { get; set; } 
        public RestaurantDTO.RestaurantDTO Restaurant { get; set; }
        public UserDTO.UserDTO Customer { get; set; }
    }
}
