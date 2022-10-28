using Domain.Models;
using Domain.Models.Order;
using Domain.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Domain.ViewModels.Order
{
    public class FilterDiscountViewModel:BasePaging<CustonizedDiscount>
    {
        public FilterDiscountEnums FilterDiscountEnums { get; set; }
        public string? Code { get; set; }
        public int? Useable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }

    public class CustonizedDiscount:BaseEntity<int>
    {
        public string DiscountCode { get; set; }
        public int DicountPercent { get; set; }
        public int? Useable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountStatus Status { get; set; }



    }

    public enum DiscountStatus
    {
        [Display(Name ="فعال")] Active,
        [Display(Name = "تاریخ استفاده از این کد تخفیف گذشته است")] ExpiredDate,
        [Display(Name = "هنوز زمان شروع این کد تخفیف آغاز نشده است")] NotStartedDate,
        [Display(Name = "تعداد کد تخفیف به پایان رسیده است")] Finished
    }

    public enum FilterDiscountEnums
    {
        [Display(Name = "همه")] All,
        [Display(Name = "تاریخ استفاده از این کد تخفیف گذشته است")] ExpiredDate,
        [Display(Name = "هنوز زمان شروع این کد تخفیف آغاز نشده است")] NotStartedDate,
        [Display(Name = "تعداد کد تخفیف به پایان رسیده است")] Finished
    }
}
