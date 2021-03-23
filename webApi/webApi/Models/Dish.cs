using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Dish
    {
        public Dish()
        {
            OrderDishes = new HashSet<OrderDish>();
            SectionDishes = new HashSet<SectionDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
        public virtual ICollection<SectionDish> SectionDishes { get; set; }
    }
}
