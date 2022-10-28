using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public class ContactUssEnums
    {
        public enum Subject
        {
            [Display(Name = "پیشنهاد")] Suggestion,
            [Display(Name = "انتقاد و شکایت")] Complaint,
            [Display(Name = "خدمات پس از فروش")] Warranty,
            [Display(Name = "پیگیری سفارش")] Track,
             [Display(Name = "حسابداری")] Accounting,
             [Display(Name = "مدیریت")] Management,
             [Display(Name = "سایر موضوعات")] etc,

        }

        public enum Status
        {
            [Display(Name = "پاسخ داده شده")] Answered,
            [Display(Name = "بدون پاسخ")] NotAnswered,

        }
    }
}
