using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductCategory:BaseEntity<int>
    {
        public int? ParnetId { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        

        #region Relations

        [ForeignKey("ParnetId")]
        public ProductCategory Category { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        #endregion
    }
}
