using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class UserRest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
