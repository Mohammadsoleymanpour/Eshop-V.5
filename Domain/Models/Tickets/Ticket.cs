using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;
using Domain.Models.UserAgg;

namespace Domain.Models.Tickets
{
    public class Ticket:BaseEntity<int>
    {
        [Required]
        public int OwnerId { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TicketStatusEnum Status { get; set; }
        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [Display(Name = "خوانده شده توسط ادمین")]
        public bool IsReadByAdmin { get; set; }
        [Display(Name = "خوانده شده توسط سازنده")]
        public bool IsReadByOwner { get; set; }
        [Display(Name = "بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TicketParts Part { get; set; }
        [Display(Name = "بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TicketLevels Levels { get; set; }

        #region Relations

        [ForeignKey("OwnerId")]
        public User User { get; set; }

    
        public List<TicketMassages> TicketMassagesList { get; set; }
        #endregion
    }
}
