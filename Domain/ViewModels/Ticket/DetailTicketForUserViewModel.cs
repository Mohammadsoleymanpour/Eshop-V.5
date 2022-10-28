using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Tickets;

namespace Domain.ViewModels.Ticket
{
    public class DetailTicketForUserViewModel
    {
        public Models.Tickets.Ticket Ticket { get; set; }
        public List<TicketMassages> Messages { get; set; }

    }
}
