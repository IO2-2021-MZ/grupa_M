using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Order
    {
        public Order()
        {
            Complaints = new HashSet<Complaint>();
            OrderDishes = new HashSet<OrderDish>();
        }

        public int Id { get; set; }
        public int PaymentMethod { get; set; }
        public int State { get; set; }
        public DateTime Date { get; set; }
        public int AddressId { get; set; }
        public int? DiscountCodeId { get; set; }
        public int? CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Address Address { get; set; }
        public virtual User Customer { get; set; }
        public virtual DiscountCode DiscountCode { get; set; }
        public virtual User Employee { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
    }
}
