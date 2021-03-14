using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Reviews = new HashSet<Review>();
            Sections = new HashSet<Section>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Rating { get; set; }
        public int State { get; set; }
        public DateTime CreationDate { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
