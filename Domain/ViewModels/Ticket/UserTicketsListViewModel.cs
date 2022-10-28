using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;
using Domain.Models.Tickets;
using Domain.ViewModels.ContactUs;
using Domain.ViewModels.Shared;
using Domain.ViewModels.User;

namespace Domain.ViewModels.Ticket
{
    public class UserTicketsListViewModel : BasePaging<Models.Tickets.Ticket>
    {
        [Required]
        public int TicketId { get; set; }

        [Display(Name = "زمان ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "آخرین بروزرسانی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime LastUpdate { get; set; }

        [Display(Name = "عنوان")]
        public string? Title { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public FilterTicketStatus Status { get; set; }
        [Display(Name = "بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public FilterTicketPart Parts { get; set; }

        [Display(Name = "سطح اهمیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public FilterTicketLevels Levels { get; set; }
    }


    public class AddTicketFromAdminViewModel
    {
        [Required]
        public int OwnerId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
       

        [Display(Name = "بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TicketParts Parts { get; set; }

        [Display(Name = "سطح اهمیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TicketLevels Levels { get; set; }

        public Task<List<GetUserForAddTicketFromAdminViewModel>>? List { get; set; }
    }

    public class TicketMassageForAdminViewModel
    {

        [Display(Name = "تاریخ ساخت")]
        public DateTime CreatDate { get; set; }
        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Massage { get; set; }
    }

    public enum FilterTicketStatus
    {
        [Display(Name = "همه")] All,
        [Display(Name = "پاسخ داده نشده")] Pending,
        [Display(Name = "پاسخ داده شده")] Answered,
        [Display(Name = "بسته شده")] Closed,
    }

    public enum FilterTicketPart
    {
        [Display(Name = "همه")] All,
        [Display(Name = "پبشنهادات")] Suggestion,
        [Display(Name = "انتقاد و شکایات")] Complaint,
        [Display(Name = "خدمات پس از فروس")] Warranty,
        [Display(Name = "پیگیری سفارش")] Track,
        [Display(Name = "مدیریت")] Management,
        [Display(Name = "حسابداری")] Accounting,
        [Display(Name = "سایر موضوعات")] etc,

    }

    public enum FilterTicketLevels
    {
        [Display(Name = "همه")] All,
        [Display(Name = "عادی")] Normal,
        [Display(Name = "مهم")] Important,
        [Display(Name = "بسیار مهم")] VeryImportant,

    }
}
