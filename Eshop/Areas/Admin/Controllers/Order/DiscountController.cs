using Application.Interface;
using Domain.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers.Order
{
    public class DiscountController : AdminBaseController
    {
        private readonly IOrderService _orderService;

        public DiscountController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("Discounts")]
        public async Task<IActionResult> Index(FilterDiscountViewModel filter)
        {
            var model = await _orderService.GetAllDiscountForAdmin(filter);
            return View(model);
        }


        #region Add

        [Route("AddDiscount")]
        public IActionResult AddDiscount()
        {
            return View();
        }

        [Route("AddDiscount")]
        [HttpPost]
        public async Task<IActionResult> AddDiscount(AddDiscountViewModel discount)
        {
            if (!ModelState.IsValid)
                return View(discount);
            var res  =await _orderService.AddDiscount(discount);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [Route("DeleteDiscount/{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {          
            var model = await _orderService.GetDiscountForAdmin(id);
            return View(model);         
        }                          
        [Route("DeleteDiscount")]     
        [HttpPost]                 
        public async Task<IActionResult> DeleteDiscount(EditDiscountViewModel discount)
        {
            if (!ModelState.IsValid)
                return View(discount);
            var res = await _orderService.DeleteDiscount(discount.Id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        [Route("EditDiscount/{id}")]
        public async Task<IActionResult> EditDiscount(int id)
        {
            var edit = await _orderService.GetDiscountForAdmin(id);
            return View(edit);
        }

        [Route("EditDiscount")]
        [HttpPost]
        public async Task<IActionResult> EditDiscount(EditDiscountViewModel edit)
        {
            if (!ModelState.IsValid)
                return View(edit);
            var result = await _orderService.EditDiscount(edit);
                
            return RedirectToAction("Index");
        }

        #endregion
    }
}
