using Domain.Models.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Role
{
    public class UserRoles:BaseEntity<int>
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion
    }
}
