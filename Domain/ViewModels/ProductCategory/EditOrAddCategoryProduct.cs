using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.ProductCategory
{
    public class EditOrAddCategoryProduct
    {
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
