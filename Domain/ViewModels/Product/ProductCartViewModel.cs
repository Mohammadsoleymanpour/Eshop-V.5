using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Product
{
    public class ProductCartViewModel
    {
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
      //  public bool IsFavorite { get; set; }

    }
}
