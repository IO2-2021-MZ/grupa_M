﻿using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class OrderDish
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int OrderId { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Section Order { get; set; }
    }
}
