using Application.Interface;
using Application.Security;
using Domain.ViewModels.Product_Tags;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers.Product
{
    [CheckPermission(Permissions.ProductTags)]
    public class ProductTagController : AdminBaseController
    {
        #region Injection

        private IProductService _productService;
        private ILoggerService _loggerService;

        public ProductTagController(IProductService productService, ILoggerService loggerService)
        {
            _productService = productService;
            _loggerService = loggerService;
        }

        #endregion

        [Route("Tags/{id}")]

        public async Task<IActionResult> Index(FilterListTagViewModel filter, int id)
        {
            ViewData["ProductId"] = id;
            var model = await _productService.GetAllProductTagsForAdmin(filter, id);
            return View(model);
        }


        #region Delete Tag

        [Route("DeleteTag/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            ViewData["ProductId"] = id;
            ViewBag.IsDeleteing = true;
            var tag = await _productService.GetTagById(id);
            return View("TagManager", tag);
        }
        [HttpPost("DeleteTag/{id}")]
        public async Task<IActionResult> DeleteTag(TagViewModel tag, int id)
        {
            ViewBag.IsDeleteing = true;
            var dbTag = await _productService.GetTagById(id);
            await _productService.DeleteTag(id);
            await _loggerService.AddLog((int)tag.TagId, User.GetUserId(), "حذف تگ");
            return Redirect("/Admin/Tags/" + id);
        }

        #endregion

        #region Add Tag To Product

        [Route("AddTag/{id}")]
        public IActionResult AddTag(int id)
        {
            ViewData["ProductId"] = id;
            return View("TagManager");
        }

        [Route("AddTag/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddTag(TagViewModel tag, int id)
        {
            if (!ModelState.IsValid)
                return View("TagManager", tag);

            var res = await _productService.AddTagToProduct(tag, id);
            await _loggerService.AddLog(res, User.GetUserId(), "افزودن تگ");
            return Redirect("/Admin/Tags/" + id);
        }

        #endregion

        #region Edit Tag

        [Route("EditTag/{id}")]
        public async Task<IActionResult> EditTag(int id)
        {
            ViewData["ProductId"] = id;
            var model = await _productService.GetTagById(id);
            return View("TagManager", model);
        }

        [HttpPost("EditTag/{id}")]
        public async Task<IActionResult> EditTag(TagViewModel tag, int id)
        {
            await _productService.UpdateTag(tag, id);
            await _loggerService.AddLog((int)tag.TagId, User.GetUserId(), "ویرایش تگ");
            return Redirect("/Admin/Tags/" + id);
        }
        #endregion 
    }
}
