using Domain.Models.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Order:BaseEntity<int>
    {
        public int UserId { get; set; }
        public bool IsFinally { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime FinalizedDate { get; set; }





        #region Relations

        [ForeignKey("UserId")]
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
