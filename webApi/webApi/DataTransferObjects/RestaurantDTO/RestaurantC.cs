using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Enums;

namespace webApi.DataTransferObjects.RestaurantDTO
{
    public class RestaurantC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public decimal Rating { get; set; }
        public string State { get; set; }
        public AddressDTO.AddressDTO Address { get; set; }
    }
}
