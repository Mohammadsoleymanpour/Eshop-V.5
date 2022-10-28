using Application.Interface;
using Domain.Interfaces;
using Domain.ViewModels.Log;
using Domain.ViewModels.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers
{
    
    public class HomeController : AdminBaseController
    {
        private IUserService _userService;
        private ITicketService _ticketService;
        private IOrderService _orderService;
        private ILoggerService _loggerService;

        public HomeController(IUserService userService, ITicketService ticketService, IOrderService orderService, ILoggerService loggerService)
        {
            _userService = userService;
            _ticketService = ticketService;
            _orderService = orderService;
            _loggerService = loggerService;
        }


        [Route("")]
        public async Task<IActionResult> Index(UserTicketsListViewModel model)
        {
            var ticket = await _ticketService.GetNoReadTickets(model);
            ViewData["OrderPartialModel"] = await _orderService.GetFinalizedOrdersForAdmin(10);
               var res= await _orderService.GetSalesOrderChartForAdmin();
               ViewData["SalesPartialModel"] = res;
            ViewData["TenLog"] = await _loggerService.GetLastTenLogs(new FilterUserLogViewModel());
            return View(ticket);
            
        }

        [Route("Logs")]
        public async Task<IActionResult> Logs(FilterUserLogViewModel model)
        {

            return View( await _loggerService.GetLog(model));
        }


        [Route("UserLogs")]
        public async Task<IActionResult> UserLogs(FilterUserLogViewModel model)
        {
            return View("LoggingUserLogin",await _loggerService.GetUserLoginLogs(model));
        }


    }
}
