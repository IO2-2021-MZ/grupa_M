using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            DiscountCodes = new HashSet<DiscountCode>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Sections = new HashSet<Section>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public decimal Rating { get; set; }
        public int State { get; set; }
        public decimal Owing { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal AggregatePayment { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
