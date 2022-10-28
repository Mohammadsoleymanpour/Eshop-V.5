using Domain.Models.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Discount : BaseEntity<int>
    {
        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }
        [Required]
        public int DicountPercent { get; set; }
        public int? Useable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }



        public List<UserDiscountCode> DiscountCodes { get; set; }
    }
}
