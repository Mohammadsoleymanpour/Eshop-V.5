using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ViewModels.Shared;

namespace Domain.ViewModels.ProductCategory
{
    public class FilterCategoryProduct:BasePaging<Models.Product.ProductCategory>
    {
        public string? CategoryName { get; set; }
        public int? ParentId { get; set; }
    }
}
