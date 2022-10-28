using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Order;
using Domain.ViewModels.Order;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        #region Admin

        Task<FilterUserOrdersForAdmin> GetAllOrdersOfUserById(FilterUserOrdersForAdmin filter);
        Task<List<Order>> GetAllFinalizedOrders(int take);

        Task<int[]> GetWeeklySalesOrderForChart();

        #endregion


        #region Site

        Task<int> AddOrderFromUser(Order order);

        Task<Order> GetOrderByUserId(int userId);

        Task<int> GetTotalPrice(int orderId);

        Task<List<OrderDetail>> GetListOrderDetailsByOrderId(int orderId);

        Task<Order> GetOrderById(int orderId);

        Task<OrderDetail> GetOrderDetailByOrderId(int orderId,int productId, int? productPriceId);

        Task<int> AddOrderDetialFromUser(OrderDetail orderDetail);
        Task<bool>EditOrderDetail(OrderDetail orderDetail);

        Task<int> AddOrderDetailProductFeature(OrderDetailProductFeature model);

        Task<bool> UpdateOrderDetail(OrderDetail model);

        Task<OrderDetail> GetOrderDetailById(int orderDetailId);


        #endregion

        #region Discount

        Task<Tuple<DiscountUseType,int>> UseDiscount(string code,int userId);

        Task<bool> UpdateDiscount(Discount discount);
        Task<bool> DeleteDiscount(int discountId);
        Task<bool> AddDiscount(Discount discount);
        Task<FilterDiscountViewModel> GetAllDiscountForAdmin(FilterDiscountViewModel filter);
        Task<Discount> GetDiscountById(int discount);

        #endregion

        #region Shared

        Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId);
      
        Task<bool> UpdateOrder(Order order);

        #endregion
    }
}
