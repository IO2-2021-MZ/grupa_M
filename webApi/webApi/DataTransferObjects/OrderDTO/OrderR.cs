using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class OrderR
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public AddressDTO.AddressDTO Address { get; set; }
        public DiscountCodeDTO.DiscountCodeDTO DiscountCode { get; set; }
        public RestaurantDTO.RestaurantDTO Restaurant { get; set; }
        public ICollection<DishDTO.PositionFromMenuDTO> Positions { get; set; }
    }
}
