using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Product;

namespace Domain.Models.Order
{
    public class OrderDetail:BaseEntity<int>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? ProductPriceId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }



        #region Relations

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product.Product Product { get; set; }

        [ForeignKey("ProductPriceId")]
        public ProductPrice? ProductPrice { get; set; }

        #endregion
    }
}
