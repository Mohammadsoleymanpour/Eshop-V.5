using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Order;

namespace Domain.Models.Product
{
    public class ProductPrice:BaseEntity<int>
    {
        public int Price { get; set; }
        public int ProductId { get; set; }





        #region Relations

        public List<ProductSelectedFeature> productSelectedFeatures { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
