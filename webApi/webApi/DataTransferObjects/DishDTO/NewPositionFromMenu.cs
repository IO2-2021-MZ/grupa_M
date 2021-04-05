using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.DataTransferObjects.DishDTO
{
    public class NewPositionFromMenu
    {
        [Required]
        public string Name { get; set }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
