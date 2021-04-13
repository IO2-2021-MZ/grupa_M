using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.ComplaintDTO
{
    public class NewComplaint
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}