using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Section
    {
        public Section()
        {
            Dishes = new HashSet<Dish>();
            OrderDishes = new HashSet<OrderDish>();
            SectionDishes = new HashSet<SectionDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
        public virtual ICollection<SectionDish> SectionDishes { get; set; }
    }
}
