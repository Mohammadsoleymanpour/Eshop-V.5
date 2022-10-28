using Application.Interface;
using Application.Security;
using Domain.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Profile.Controllers
{
    public class OrderController : BaseProfileController
    {
        IOrderService _orderService;
        IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        [Route("Orders/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var res = await _orderService.GetOrderByUserId(id);
            var TotPrice = await _orderService.GetTotalPrice(res.Id);
            ViewBag.TotalPrice = TotPrice;
            return View(res);
        }
        [Route("Orders/{id}")]
        [HttpPost]
        public  IActionResult Index(Order model,int price)
        {
        
            var payment = new ZarinpalSandbox.Payment(price);

            var res = payment.PaymentRequest("پرداخت سفارش", "https://localhost:44348/OnlinePayment/" + model.Id);
            
            ViewBag.OrderId = model.Id;

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            return View("~/Views/Home/onlinePayment.cshtml");
        }
        [Authorize]
        [Route("BuyProduct/{id}/{productPriceId?}")]
        public async Task<IActionResult> BuyProduct(int id, int? productPriceId)
        {

            var orderId = await _orderService.AddOrderFromUser(User.GetUserId(), id, productPriceId);

            if (orderId == null)
            {
                return BadRequest();
            }

            return Redirect("/Profile/Orders/" + User.GetUserId() + "?addproduct=true");
        }

        [Route("OrderDetail/{id}")]
        public async Task<IActionResult> ShowOrderDetail(int id)
        {
            var orderDetails = await _orderService.GetListOrderDetailsByOrderId(id);
            if (orderDetails == null)
            {
                return BadRequest();
            }
            return View(orderDetails);
        }
        [Authorize]
        [Route("IncraeseCount/{id}")]
        public async Task<IActionResult> IncraeseCount(int id)
        {

            var orderDetail = await _orderService.GetOrderDetailById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.Count += 1;

            var res = await _orderService.UpdateOrderDetail(orderDetail);

            return Redirect("/Profile/Orders/" + User.GetUserId());
        }
        [Authorize]
        [Route("DecreaseCount/{id}")]
        public async Task<IActionResult> DecreaseCount(int id)
        {

            var orderDetail = await _orderService.GetOrderDetailById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.Count -= 1;

            var res = await _orderService.UpdateOrderDetail(orderDetail);

            return Redirect("/Profile/Orders/" + User.GetUserId());
        }
        [Authorize]
        [Route("DeleteOrderDetail/{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {

            var orderDetail = await _orderService.GetOrderDetailById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            orderDetail.IsDelete = true;

            var res = await _orderService.UpdateOrderDetail(orderDetail);

            return Redirect("/Profile/Orders/" + User.GetUserId());
        }
        [Authorize]
        [Route("UseDiscount/{id}")]
        public async Task<IActionResult> UseDiscount(int id, string code)
        {
            var discount = await _orderService.UseDiscount(User.GetUserId(),
                await _orderService.GetTotalPrice(id), code);

            ModelState.AddModelError("Discount", discount.Item1.GetEnumDisPlayName());
            ViewBag.DiscountPrice = discount.Item2;
            return Redirect("Orders/" + id);
        }
    }
}
