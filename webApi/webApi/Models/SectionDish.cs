using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class SectionDish
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int DishId { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Section Section { get; set; }
    }
}
