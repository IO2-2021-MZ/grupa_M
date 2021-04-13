using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.ReviewDTO
{
    public class NewReview
    {
        public string Content { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
    }
}
