using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;

namespace Domain.ViewModels.Ticket
{
    public class AddTicketFromUserViewModel
    {
        public string Subject { get; set; }
        public TicketLevels TicketLevels { get; set; }
        public TicketParts  TicketParts { get; set; }
        public string Message { get; set; }
    }
}
