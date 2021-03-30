using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.RestaurantDTO
{
    public class NewRestaurant
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string ContactInformation { get; set; }
        [Required]
        public AddressDTO.AddressDTO Address { get; set; }
    }
}
