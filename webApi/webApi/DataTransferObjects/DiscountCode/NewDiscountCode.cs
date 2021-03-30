using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.DataTransferObjects.DiscountCodeDTO
{
    public class NewDiscountCode
    {
        public NewDiscountCode()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
