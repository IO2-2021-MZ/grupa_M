using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Complaint
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Answer { get; set; }
        public int State { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }

        public virtual Section Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
