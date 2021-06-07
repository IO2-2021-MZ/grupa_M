using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.DataTransferObjects.UserDTO
{
    public class Employee:UserDTO
    {
        public bool isRestaurateur { get; set; }
        public virtual RestaurantDTO.RestaurantDTO restaurant { get; set; }
    }
}
