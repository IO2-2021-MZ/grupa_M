using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.Enums;

namespace webApi.DataTransferObjects.RestaurantDTO
{
    public class RestaurantC

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
        public string State { get; set; }
        [Required]
        public AddressDTO.AddressDTO Address { get; set; }
    }
}
