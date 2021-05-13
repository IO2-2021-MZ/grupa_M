using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.ReviewDTO
{
    public class ReviewR
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public decimal Rating { get; set; }
    }
}
