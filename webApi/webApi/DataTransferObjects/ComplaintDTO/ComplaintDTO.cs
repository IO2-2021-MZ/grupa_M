using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.ComplaintDTO
{
    public class ComplaintDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
        public decimal Response { get; set; }
=======
        public string Response { get; set; }
>>>>>>> 5ea878217fb3a18ef8db7e9c7c2da9c10ded339d
=======
        public string Response { get; set; }
>>>>>>> WorkingComplaintTests
        [Required]
        public bool Open { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}