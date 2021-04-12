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
        public string Response { get; set; }
        [Required]
        public bool Open { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}