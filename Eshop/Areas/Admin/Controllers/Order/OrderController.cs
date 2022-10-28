using Application.Interface;
using Domain.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using Eshop.Common;

namespace Eshop.Areas.Admin.Controllers.Order
{
    public class OrderController : AdminBaseController
    {
        #region Injections

        private IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }



        #endregion

        [CheckPermission(Permissions.UserOrders)]
        [Route("Orders/{id}")]
        public async Task<IActionResult> Orders(FilterUserOrdersForAdmin filter,int id)
        {
            filter.UserId = id;
            var model = await _orderService.GetAllOrdersOfUserForAdmin(filter);

            return View(model);
        }

        [Route("OderDetail/{orderId}")]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var order = await _orderService.GetOrderDetailForAdminById(orderId);
            return View(order);
        }

    }
}
