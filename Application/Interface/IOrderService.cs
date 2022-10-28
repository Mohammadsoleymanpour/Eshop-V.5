using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Order;
using Domain.ViewModels.Order;

namespace Application.Interface
{
    public interface IOrderService
    {

        #region Admin

        Task<FilterUserOrdersForAdmin> GetAllOrdersOfUserForAdmin(FilterUserOrdersForAdmin filter);
        Task<OrderDetailForAdminViewModel> GetOrderDetailForAdminById(int orderId);
        Task<OrderListPartialViewModel> GetFinalizedOrdersForAdmin(int take);
        Task<SalesOrderChartViewModel> GetSalesOrderChartForAdmin();

        #endregion

        #region site
        Task<int> GetTotalPrice(int orderId);
        Task<Order> GetOrderByUserId(int userId);
        Task<int> AddOrderFromUser(int userId, int productId, int? productPriceId);

        Task<List<OrderDetail>> GetListOrderDetailsByOrderId(int orderId);
        Task<int> AddOrderDetailProductFeature(OrderDetailProductFeature model);

        Task<bool> UpdateOrderDetail(OrderDetail model);

        Task<OrderDetail> GetOrderDetailById(int orderDetailId);

        #endregion

        #region Discount

        Task<Tuple<DiscountUseType, int>> UseDiscount(int userId, int totalPrice, string code);
        Task<bool> DeleteDiscount(int discountId);
        Task<bool> AddDiscount(AddDiscountViewModel discount);
        Task<bool> EditDiscount(EditDiscountViewModel editDiscount);
        Task<FilterDiscountViewModel> GetAllDiscountForAdmin(FilterDiscountViewModel filter);
        Task<EditDiscountViewModel> GetDiscountForAdmin(int discountId);

        #endregion

        #region Shared

        Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<bool> UpdateOrder(Order order);

        #endregion
    }
}
