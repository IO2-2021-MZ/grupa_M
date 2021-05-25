using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.AddressDTO;
using webApi.Enums;

namespace webApi.DataTransferObjects.RestaurantDTO
{
    public class RestaurantDTO:RestaurantC
    {
        [Required]
        public decimal Owing { get; set; }
        [Required]
        public decimal AggregatePayment { get; set; }
    }
}
