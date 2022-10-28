using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using Application.Interface;
using Application.Sender;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.User;

namespace Eshop.ViewComponents
{
    public class Order : ViewComponent
    {
        private IOrderService _orderService;

        public Order(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var order = await _orderService.GetOrderByUserId(id);
            if (order != null)
            {

                int? TotPrice = await _orderService.GetTotalPrice(order.Id);
                ViewBag.TotalPrice = TotPrice;
            }



            if (User.Identity.IsAuthenticated)
            {

                return await Task.FromResult((IViewComponentResult)View("OrderViewComponent", order));
            }

            return null;
        }
    }
}
