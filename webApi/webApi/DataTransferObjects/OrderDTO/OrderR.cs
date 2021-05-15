using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class OrderR:OrderDTO
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
        public decimal OriginalPrice { get; set; }
        [Required]
        public decimal FinalPrice { get; set; }
        [Required]
        public AddressDTO.AddressDTO Address { get; set; }
        [Required]
        public DiscountCodeDTO.DiscountCodeDTO DiscountCode { get; set; }
        [Required]
        public RestaurantDTO.RestaurantDTO Restaurant { get; set; }
        [Required]
        public ICollection<DishDTO.PositionFromMenuDTO> Positions { get; set; }
        public UserDTO.UserDTO Employee { get; set; }
    }
}
