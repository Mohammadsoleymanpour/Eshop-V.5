using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Order
{
    public enum DiscountUseType
    {
        [Display(Name ="کد تخفیف اعمال شد")]Success,
        [Display(Name ="تاریخ استفاده از این کد تخفیف گذشته است")]ExpiredDate,
        [Display(Name ="هنوز زمان شروع این کد تخفیف آغاز نشده است")]NotStartedDate,
        [Display(Name ="تعداد کد تخفیف به پایان رسیده است")]Finished,
        [Display(Name ="کد تخفیف یافت نشد")]NotFound,
        [Display(Name = "کد تخفیف قبلا استفاده شده است")] UserUsed

    }
}
