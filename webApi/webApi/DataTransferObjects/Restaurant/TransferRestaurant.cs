using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Address;
using webApi.Enums;

namespace webApi.DataTransferObjects.Restaurant
{
    public class TransferRestaurant
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string ContactInformation { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Required]
        public RestaurantState State { get; set; }
        [Required]
        public decimal Owing { get; set; }
        [Required]
        public decimal AggregatePayment { get; set; }
        [Required]
        public Address.TransferAddress Address { get; set; }
    }
}
