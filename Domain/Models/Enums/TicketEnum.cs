using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public enum TicketStatusEnum
    {
        [Display(Name = "پاسخ داده شده")]
        Answered,
        [Display(Name = "در حال بررسی")]
        Pending,
        [Display(Name = "بسته شده")]
        Closed

    }

    public enum TicketParts
    {
        [Display(Name = "پیشنهاد")] Suggestion,
        [Display(Name = "انتقاد و شکایت")] Complaint,
        [Display(Name = "خدمات پس از فروش")] Warranty,
        [Display(Name = "پیگیری سفارش")] Track,
        [Display(Name = "حسابداری")] Accounting,
        [Display(Name = "مدیریت")] Management,
        [Display(Name = "سایر موضوعات")] etc,
    }

    public enum TicketLevels
    {
        [Display(Name = "عادی")] Normal,
        [Display(Name = "مهم")] Important,
        [Display(Name = "بسیار مهم")] VeryImportant,
       
    }
}
