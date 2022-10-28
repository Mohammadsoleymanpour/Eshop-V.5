
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Interface;
using Eshop.Controllers.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Services;
using Domain.ViewModels.DynamicPage;

namespace Eshop.Controllers
{
    public class HomeController : BaseSiteController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private IOrderService _orderService;
        private IDynamicLinkService _dynamicLinkService;
        private IFaqService _faqService;
        private IDynamicPageService _pageService;
        private IBannerService _bannerService;
        private IVoteService _voteService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IOrderService orderService, IDynamicLinkService dynamicLinkService, IFaqService faqService, IDynamicPageService pageService, IBannerService bannerService, IVoteService voteService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            _dynamicLinkService = dynamicLinkService;
            _faqService = faqService;
            _pageService = pageService;
            _bannerService = bannerService;
            _voteService = voteService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _productService.GetAllProductForIndex();
            var header = await _dynamicLinkService.GetLinksForHeader();
            var footer = await _dynamicLinkService.GetLinksForFooter();
            var banner = await _bannerService.GetBannerListAsync();
            ViewData["HeaderLinks"] = header;
            ViewData["FooterLinks"] = footer;
            ViewData["Products"] = res;
            ViewData["Banners"] = banner;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> GetSubGroups(int id)
        {
            var subcat = _productService.GetSubCategoriesForAddProductAdmin(id);
            return Json(new SelectList(await subcat, "Value", "Text"));
        }

        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> onlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var order = await _orderService.GetOrderById(id);
                ViewBag.OrderId = order.Id;
                var payment = new ZarinpalSandbox.Payment(await _orderService.GetTotalPrice(id));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {

                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    order.IsFinally = true;
                    order.TrackingNumber = res.RefId.ToString();
                    order.FinalizedDate = DateTime.Now;
                    await _orderService.UpdateOrder(order);
                }

            }

            return View();
        }
        [Route("faq")]
        public async Task<IActionResult> FAQ()
        {
            return View(await _faqService.GetFaqListAsync());
        }



        [Route("Page/{title}")]
        public async Task<IActionResult> Page(string title)
        {
            var link = await _pageService.GetPageByTitle(title);
            return View(link);


        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });

        }
       
    }
}