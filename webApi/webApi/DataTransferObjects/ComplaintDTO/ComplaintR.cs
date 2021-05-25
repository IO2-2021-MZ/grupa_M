using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.DataTransferObjects.ComplaintDTO
{
    public class ComplaintR
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public string Response { get; set; }
        [Required]
        public bool Open { get; set; }
        [Required]
        public int OrderId { get; set; }
        public UserDTO.UserDTO Employee { get; set; }
        [Required]
        //customerId odziedziczy też ComplaintDTO, no ale już trudno, nie bedzie uzywane, a wygodniej jest zeby jedna byla klasą bazową drugiej
        public int CustomerId { get; set; }
    }
}