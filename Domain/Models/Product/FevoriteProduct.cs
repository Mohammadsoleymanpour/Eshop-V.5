using Domain.Models.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class FavoriteProduct:BaseEntity<int>
    {
        public int  ProductId { get; set; }

        public int UserId { get; set; }

        #region Relations
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
