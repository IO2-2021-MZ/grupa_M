using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.UserDTO
{
    public class NewCustomer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public AddressDTO.AddressDTO address { get; set; }
    }
}
