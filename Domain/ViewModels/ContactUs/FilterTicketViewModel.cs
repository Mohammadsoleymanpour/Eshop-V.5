using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Models.ContactUss;

namespace Domain.ViewModels.ContactUs
{
    public class FilterContactUssViewModel : BasePaging<ContactUss>
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public FilterContactUssStatus Status { get; set; }
        public FilterContactUssSubject Subject { get; set; }

    }


    public enum FilterContactUssStatus
    {
        [Display(Name = "همه")] All,
        [Display(Name = "پاسخ داده نشده")] NotAnswered,
        [Display(Name = "پاسخ داده شده")] Answered,
    }

    public enum FilterContactUssSubject
    {
        [Display(Name = "همه")] All,
        [Display(Name = "پبشنهادات")] Suggestion,
        [Display(Name = "انتقاد و شکایات")] Complaint,
        [Display(Name = "خدمات پس از فروس")] Warranty,
        [Display(Name = "پیگیری سفارش")] Track,
        [Display(Name = "مدیریت")] Managment,
        [Display(Name = "حسابداری")] Accounting,
        [Display(Name = "سایر موضوعات")] Etc,

    }

}