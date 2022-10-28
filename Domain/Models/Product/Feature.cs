using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;

namespace Domain.Models.Product
{
    public class Feature:BaseEntity<int>
    {
    
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        
        public FeatureEnums Type { get; set; }

        #region Relations

        public List<FeatureValue> FeatureValues { get; set; }

        public List<ProductSelectedFeature> productSelectedFeatures { get; set; }

        #endregion

    }
}
