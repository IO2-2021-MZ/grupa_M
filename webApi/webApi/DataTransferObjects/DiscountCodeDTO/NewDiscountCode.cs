using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.OrderDTO;
using webApi.Models;

namespace webApi.DataTransferObjects.DiscountCodeDTO
{
    public class NewDiscountCode
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
    }
}
