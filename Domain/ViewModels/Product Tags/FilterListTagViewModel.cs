using Domain.Models.Product;
using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Product_Tags
{
    public class FilterListTagViewModel :BasePaging<ProductTag>
    {
        public string TagName { get; set; }
        public int? ProductId { get; set;}

    }
}
