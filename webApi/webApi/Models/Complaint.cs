using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Complaint
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Response { get; set; }
        public bool Open { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }

        public virtual User Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
