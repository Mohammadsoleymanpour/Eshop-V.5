using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Order;
using Domain.ViewModels.Shared;

namespace Domain.ViewModels.Order
{
    public class OrderDetailForAdminViewModel
    {
        // public int TotalPriceOfProduct { get; set; }
        public int OrderId { get; set; }
        public int TotalPriceOfOrder { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
       // public string ProductImage { get; set; }
    }
}
