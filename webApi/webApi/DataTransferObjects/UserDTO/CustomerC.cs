using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.RestaurantDTO;
namespace webApi.DataTransferObjects.UserDTO
{
    public class CustomerC : UserDTO
    {
        public AddressDTO.AddressDTO Address { get; set; }
        public List<RestaurantC> FavouriteRestaurants { get; set; }
    }
}
