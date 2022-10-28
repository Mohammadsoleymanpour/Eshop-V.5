using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;
using Domain.Models.Product;
using Domain.Models.Tickets;
using Domain.Models.Order;
using Domain.Models.Common;
using Domain.Models.Votes;

namespace Domain.Models.UserAgg
{
    public class User : BaseEntity<int>
    {
        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? LastName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد .")]
        public string PhoneNumber { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        [Display(Name = "کد فعالسازی")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required]
        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت")]
        [Required]
        public Status Status { get; set; }
        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Gender Gender { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "ادمین")]
        public bool IsAdmin { get; set; }

        #region Relatoins

        public List<FavoriteProduct> FavoriteProducts { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<TicketMassages> TicketMassagesList { get; set; }
        public List<Order.Order> Orders { get; set; }
        public List<Log> Logs { get; set; }
        public List<ProductVotes> ProductVotesList { get; set; }
        public List<CommentVote> CommentVotes { get; set; }
        public List<UserDiscountCode> DiscountCodes { get; set; }

        #endregion

    }
}
