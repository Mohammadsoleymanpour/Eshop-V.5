using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductSelectedFeature:BaseEntity<int>
    {
        public int ProductPriceId { get; set; }
        public int FeatureId { get; set; }
        public int FeatureValueId { get; set; }



        #region Relations

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        [ForeignKey("FeatureValueId")]
        public FeatureValue featureValue { get; set; }

        [ForeignKey("ProductPriceId")]
        public ProductPrice productPrice { get; set; }

        #endregion
    }
}
