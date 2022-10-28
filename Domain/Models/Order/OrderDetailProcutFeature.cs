using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class OrderDetailProductFeature:BaseEntity<int>
    {

        public string FeatureTitle { get; set; }
        public string FeatureValue { get; set; }

        public int OrderDetailId { get; set; }

        #region Relations

        public OrderDetail OrderDetail { get; set; }

        #endregion
    }
}
