using System;
using System.Collections.Generic;

#nullable disable

namespace webApi.Models
{
    public partial class Section
    {
        public Section()
        {
            Complaints = new HashSet<Complaint>();
            Dishes = new HashSet<Dish>();
            SectionDishes = new HashSet<SectionDish>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        public virtual ICollection<SectionDish> SectionDishes { get; set; }
    }
}
