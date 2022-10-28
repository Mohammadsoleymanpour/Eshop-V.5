using System.Collections.Immutable;
using Application.Interface;
using Application.Security;
using Domain.ViewModels.Product;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eshop.Areas.Admin.Controllers.Product
{
    public class ProductController : AdminBaseController
    {
        private IProductService _productService;
        private ILoggerService _loggerService;

        public ProductController(IProductService productService, ILoggerService loggerService)
        {
            _productService = productService;
            _loggerService = loggerService;
        }

        [Route("Product")]
        [CheckPermission(Permissions.ProductManagement)]
        public async Task<IActionResult> Index(FilterProductViewModel filter, int startPrice = 0, int endPrice = 0)
        {
            var result = await _productService.GetProductForAdmin(filter, startPrice, endPrice);

            return View(result);
        }

        #region Add

        [Route("AddProduct")]
        [CheckPermission(Permissions.AddProduct)]
        public async Task<IActionResult> AddProduct()
        {
            var cat = await _productService.GetCategoriesForAddProductAdmin();
            ViewData["Categories"] = new SelectList(cat, "Value", "Text");
            var subcat = await _productService.GetSubCategoriesForAddProductAdmin(int.Parse(cat.First().Value));
            ViewData["SubCategories"] = new SelectList(subcat, "Value", "Text");

            ViewData["Features"] = await _productService.GetFeaturesForAddProduct();
            ViewData["FeaturesValues"] = await _productService.GetAllFeatureValues();

            return View();
        }

        [Route("AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreatProductViewModel model, List<int> selctedcategor, List<int> feacherValues)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            model.SubGroupId = selctedcategor;
            model.FeatureValues = feacherValues;
            int id = await _productService.AddProductFromAdmin(model);
            if (id == null)
            {
                return NotFound();
            }
            await _loggerService.AddLog(id, User.GetUserId(), "افزودن محصول");
            return Redirect("/Admin/Product");
        }

        #endregion

        #region Edit

        [Route("EditProduct/{id}")]
        [CheckPermission(Permissions.EditProduct)]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductFromAdmin(id);
            var cat = await _productService.GetCategoriesForAddProductAdmin();
            ViewData["Categories"] = new SelectList(cat, "Value", "Text");
            var subcat = await _productService.GetAllSubCategoriesForEditProduct(product.SubGroupId.First());
            ViewData["SubCategories"] = new SelectList(subcat, "Value", "Text");
            ViewData["Features"] = await _productService.GetFeaturesForAddProduct();
            ViewData["FeaturesValues"] = await _productService.GetAllFeatureValues();
            ViewBag.PrId = id;
            return View(product);
        }

        [Route("EditProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model, List<int> selctedcategor, List<int> feacherValues)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.SubGroupId = selctedcategor;
            model.FeatureValues = feacherValues;

            if (!await _productService.UpdateProductFromAdmin(model))
            {
                return NotFound();
            }
            await _loggerService.AddLog(model.Id, User.GetUserId(), "ویرایش محصول");
            return Redirect("/Admin/Product");
        }
        #endregion

        #region Delete

        [Route("DeleteProduct/{id}")]
        [CheckPermission(Permissions.DeleteProduct)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductFromAdmin(id);
            return View(product);
        }
        [Route("DeleteProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(EditProductViewModel model)
        {
            var res = await _productService.DeleteProductFromAdmin(model);
            if (res == false)
            {
                return NotFound();
            }
            await _loggerService.AddLog(model.Id, User.GetUserId(), "حذف محصول");
            return Redirect("/Admin/Product");
        }
        #endregion


        [Route("ReturnFeatureJson/{id}")]
        public async Task<IActionResult> FeaturePartial(int id)
        {
            var res = await _productService.GetFeaturesForAddProduct();
            ViewData["Features"] = res;
            ViewData["productId"] = id;
            var result = await _productService.GetAllFeaturesSelected(id);
            ViewData["ProductFeature"] = result;
            ViewData["FeaturesValues"] = await _productService.GetAllFeatureValues();
            var editViewModel = await _productService.GetProductFromAdmin(id);
            return PartialView("ProductFeaturePartial", editViewModel);
        }


        [Route("AddFeatureJson")]
        [HttpPost]
        public async Task<IActionResult> AddFeature(List<int> featureValues, int plusPrice, int productId)
        {
            var res = await _productService.AddFeatureToProduct(featureValues, plusPrice, productId);

            return RedirectToAction("FeaturePartial");
        }


        [Route("RemoveFeatureResult")]
        [HttpPost]
        public async Task<IActionResult> RemoveFeatureResult(int priceId)
        {
            var res = await _productService.DeleteFeatureFromProduct(priceId);
            return RedirectToAction("FeaturePartial");

        }
    }
}
