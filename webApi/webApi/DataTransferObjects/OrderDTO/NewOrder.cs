using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.OrderDTO
{
    public class NewOrder
    {
        [Required]
        public int PaymentMethod { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public AddressDTO.AddressDTO Address { get; set; }
        [Required]
        public int DiscountCodeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        //[Required]
        //public ICollection<DishDTO.PositionFromMenuDTO> Positions { get; set; }

    }
}
