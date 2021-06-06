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
        public string PaymentMethod { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public AddressDTO.AddressDTO Address { get; set; }
        public int? DiscountCodeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int[] PositionsIds { get; set; }

    }
}
