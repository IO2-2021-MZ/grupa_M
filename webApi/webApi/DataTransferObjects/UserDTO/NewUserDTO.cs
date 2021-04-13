using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.DataTransferObjects.UserDTO
{
    public class NewUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsRestaurateur { get; set; }
        public bool IsAdministrator { get; set; }
        public virtual Address Address { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
