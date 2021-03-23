using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class DiscountCode
    {
        public DiscountCode()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int Percent { get; set; }
        public string Code { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
