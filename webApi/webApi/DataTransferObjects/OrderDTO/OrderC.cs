using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class OrderC:OrderDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public AddressDTO.AddressDTO Address { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public RestaurantDTO.RestaurantDTO Restaurant { get; set; }
        public DishDTO.PositionFromMenuDTO[] Positions { get; set; }
    }
}
