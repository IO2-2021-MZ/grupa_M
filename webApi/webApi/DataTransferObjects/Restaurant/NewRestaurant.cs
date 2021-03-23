using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.Restaurant
{
    public class NewRestaurant
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string ContactInformation { get; set; }
        [Required]
        public Address.Address Address { get; set; }
    }
}
