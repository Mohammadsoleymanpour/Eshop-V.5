using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Role
{
    public class Permission:BaseEntity<int>
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public Permission? Permission1 { get; set; }

        public List<RolePermission> RolePermission { get; set; }
        #endregion
    }
}
