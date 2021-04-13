using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.DataTransferObjects.ReviewDTO
{
    public class ReviewDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int RestaurantId { get; set; }

    }
}
