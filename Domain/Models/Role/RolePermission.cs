using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Role
{
    public class RolePermission:BaseEntity<int>
    {

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        #region Relations
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        #endregion
    }
}
