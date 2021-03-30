using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.DishDTO;

namespace webApi.DataTransferObjects.SectionDTO
{
    public class SectionDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public ICollection<PositionFromMenuDTO> Positions { get; set; }
    }
}
