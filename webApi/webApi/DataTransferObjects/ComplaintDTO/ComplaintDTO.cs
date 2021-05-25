using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.ComplaintDTO
{
    public class ComplaintDTO:ComplaintR
    {
        [Required]
        public int CustomerId { get; set; }
    }
}