using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class OrderC:OrderDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public AddressDTO.AddressDTO Address { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        [Required]
        public decimal FinalPrice { get; set; }
        [Required]
        public RestaurantDTO.RestaurantDTO Restaurant { get; set; }
        [Required]
        public DishDTO.PositionFromMenuDTO[] Positions { get; set; }
    }
}
