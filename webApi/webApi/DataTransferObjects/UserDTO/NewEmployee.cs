using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.UserDTO
{
    public class NewEmployee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool isRestaurateur { get; set; }
        public int? restaurantId { get; set; }
    }
}
