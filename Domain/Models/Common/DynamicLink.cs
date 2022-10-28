using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
    public class DynamicLink:BaseEntity<int>
    {
        public string Title { get; set; }
        public string LinkUrl { get; set; }
        public PositionLinks Position { get; set; }
        public DateTime? ExpirationDate { get; set; }

        #region Relations

        public DynamicPage? DynamicPage { get; set; }

        #endregion
    }
}
