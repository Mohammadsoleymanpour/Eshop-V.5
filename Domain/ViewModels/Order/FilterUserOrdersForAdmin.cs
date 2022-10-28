using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ViewModels.Shared;

namespace Domain.ViewModels.Order
{
    public class FilterUserOrdersForAdmin:BasePaging<Models.Order.Order>
    {
        public string? TrackingNumber { get; set; }

        public int UserId { get; set; }

        public FilterStatusOrderFinally Status { get; set; }
    }

    public enum FilterStatusOrderFinally
    {
        [Display(Name = "همه")] All,
        [Display(Name = "نهایی شده")] Finalized,
        [Display(Name = "باز")] NotFinalized
    }
}
