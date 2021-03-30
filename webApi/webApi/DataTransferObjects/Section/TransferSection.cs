using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Dish;

namespace webApi.DataTransferObjects.Section
{
    public class TransferSection
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public ICollection<TransferPositionFromMenu> Positions { get; set; }
    }
}
