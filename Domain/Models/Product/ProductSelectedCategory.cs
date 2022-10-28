using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductSelectedCategory:BaseEntity<int>
    {

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        #region Relations
        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion
    }
}
