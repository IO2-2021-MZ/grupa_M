using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public decimal Rating { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }

        public virtual User Customer { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
