using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class FeatureValue:BaseEntity<int>
    {
        public int FeatureId { get; set; }

        public string Value { get; set; }

        #region Relations

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public List<ProductSelectedFeature> productSelectedFeatures { get; set; }

        #endregion
    }
}
