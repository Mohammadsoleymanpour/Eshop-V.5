using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.UserAgg
{
    public class UserDiscountCode : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int DiscountId { get; set; }



        #region Relations

        public User User { get; set; }
        public Order.Discount Discount { get; set; }

        #endregion
    }
}
