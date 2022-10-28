using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductTag : BaseEntity<int>
    {
        [Display(Name = "تگ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TagName { get; set; }

        public int ProductId { get; set; }



        #region Relations

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion
    }
}
