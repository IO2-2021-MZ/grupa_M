using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class User
    {
        public User()
        {
            ComplaintCustomers = new HashSet<Complaint>();
            ComplaintEmployees = new HashSet<Complaint>();
            OrderCustomers = new HashSet<Order>();
            OrderEmployees = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsRestaurateur { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime CreationDate { get; set; }
        public string PasswordHash { get; set; }
        public int? AddressId { get; set; }
        public int? RestaurantId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Complaint> ComplaintCustomers { get; set; }
        public virtual ICollection<Complaint> ComplaintEmployees { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<Order> OrderEmployees { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
