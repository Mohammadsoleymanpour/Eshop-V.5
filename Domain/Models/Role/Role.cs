using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Role
{
    public class Role:BaseEntity<int>   
    {
        public string RoleTitle { get; set; }

        //yufuguyfuyifyu
        #region Relations

        public List<UserRoles> UserRole { get; set; }
        public List<RolePermission> RolePermission { get; set; }

        #endregion

    }
}
