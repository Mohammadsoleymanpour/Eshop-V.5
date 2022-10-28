using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.ViewModels.Shared;

namespace Domain.ViewModels.Ticket
{
    public class FilterTicketListViewModel : BasePaging<Models.Tickets.Ticket>
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public FilterTicketPart FilterTicketPart { get; set; }
        public FilterTicketStatus FilterTicketStatus { get; set; }

    }


 
}
