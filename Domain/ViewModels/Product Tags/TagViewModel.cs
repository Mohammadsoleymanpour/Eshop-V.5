using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Product_Tags
{
    public class TagViewModel
    {
        public int? TagId { get; set; }
        public string TagName { get; set; }
        public bool? IsDelete { get; set; }
        public int? ProductId { get; set;}

    }
}
