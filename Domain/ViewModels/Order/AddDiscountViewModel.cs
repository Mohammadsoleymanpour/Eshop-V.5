using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Order
{
    public class AddDiscountViewModel
    {
        public string DiscountCode { get; set; }
        public int DicountPercent { get; set; }
        public int? Useable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountStatus? Status { get; set; }
    }
}
