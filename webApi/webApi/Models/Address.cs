using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
            Restaurants = new HashSet<Restaurant>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
