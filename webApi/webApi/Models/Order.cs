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
        }

        public int Id { get; set; }
        public int PaymentMethod { get; set; }
        public int OrderState { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public int CodeId { get; set; }

        public virtual Address Address { get; set; }
        public virtual DiscountCode Code { get; set; }
        public virtual User Customer { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
