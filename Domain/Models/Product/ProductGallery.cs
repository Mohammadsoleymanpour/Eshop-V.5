using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models.Product
{
    public class ProductGallery:BaseEntity<int>
    {
        public string ImageName { get; set; }

        public int ProductId { get; set; }

        public bool IsDefault { get; set; }

        #region Relations
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion
    }
}
