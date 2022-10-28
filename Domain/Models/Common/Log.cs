using Domain.Models.Enums;
using Domain.Models.Product;
using Domain.Models.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
    public class Log:BaseEntity<int>
    {
        public int UserId { get; set; }
        public string Desctiption { get; set; }
        public int EntityId { get; set; }
        public LogType LogType { get; set; }


        #region Relations

        [ForeignKey("UserId")]
        public User User { get; set; }



        #endregion

    }
}
