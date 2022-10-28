using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public class TicketEnums
    {
        public enum AddTicket
        {
            [Display(Name = "عملیات با موفقیت انجام شد.")] Success,
            [Display(Name = "عملیات با شکست مواجه شد.")] Failed
        }
    }
}
