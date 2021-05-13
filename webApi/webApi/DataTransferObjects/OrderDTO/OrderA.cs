using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class OrderA:OrderDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public AddressDTO.AddressDTO Address { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public RestaurantDTO.RestaurantDTO Restaurant { get; set; }
        public UserDTO.UserDTO Customer { get; set; }
    }
}
