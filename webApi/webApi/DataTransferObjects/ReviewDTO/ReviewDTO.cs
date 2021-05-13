using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.DataTransferObjects.ReviewDTO
{
    public class ReviewDTO: ReviewR
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int RestaurantId { get; set; }

    }
}
