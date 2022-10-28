using Domain.Models.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Tickets
{
    public class TicketMassages:BaseEntity<int>
    {
      [Required]
        public int TicketId { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Message { get; set; }

        #region Relations
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
        [ForeignKey("SenderId")]
        public User User { get; set; }

        #endregion
    }
}
