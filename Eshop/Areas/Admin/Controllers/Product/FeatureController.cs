using Application.Interface;
using Domain.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using Eshop.Common;

namespace Eshop.Areas.Admin.Controllers.Product
{
    [CheckPermission(Permissions.FeatureManagement)]
    public class FeatureController : AdminBaseController
    {
        private IProductService _productService;
        private ILoggerService _loggerService;

        public FeatureController(IProductService productService, ILoggerService loggerService)
        {
            _productService = productService;
            _loggerService = loggerService;
        }

        [Route("Feature")]
        public async Task<IActionResult> Index(FeatureViewModel model)
        {

            return View(await _productService.GetAllFeatureForAdmin(model));
        }

        #region Add

        [Route("AddFeature")]
        public async Task<IActionResult> AddFeature()
        {

            return View();
        }

        [Route("AddFeature")]
        [HttpPost]
        public async Task<IActionResult> AddFeature(AddFeatureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            int id = await _productService.AddFeatureFromAdmin(model);
            if (id == 0)
            {
                return BadRequest();

            }
            await _loggerService.AddLog(id, User.GetUserId(), "افزودن ویژگی");
            return Redirect("/Admin/Feature");
        }

        #endregion

        #region Edit

        [Route("EditFeature/{id}")]
        public async Task<IActionResult> EditFeature(int id)
        {
            var res = await _productService.GetFeatureById(id);
            return View(res);
        }

        [Route("EditFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditFeature(EditFeatureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productService.EditFeatureFromAdmin(model);
            if (result!=true)
            {
                return NotFound();
            }
            await _loggerService.AddLog((int)model.Id, User.GetUserId(), "ویرایش ویژگی");
            return Redirect("/Admin/Feature");

        }

        #endregion

        #region Delete

        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var res = await _productService.GetFeatureById(id);
            return View(res);
        }

        [Route("DeleteFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteFeature(EditFeatureViewModel model)
        {
            var res = await _productService.DeleteFeature(model.Id);
            if (res != true)
            {
                return NotFound();
            }
            await _loggerService.AddLog((int)model.Id, User.GetUserId(), "حذف ویژگی");
            return Redirect("/Admin/Feature");
        }

        #endregion

        [Route("Values/{id}")]
        public async Task<IActionResult> Values(int id)
        {
            ViewBag.Id = id;
            return View(await _productService.GetAllValues(id));
        }


        [Route("AddFeatureValue/{id}")]
        public async Task<IActionResult> AddFeatureValue(int id)
        {

            return View(new AddFeatureValueViewModel()
            {
                FeatureId = id,

            });
        }

        [Route("AddFeatureValue/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddFeatureValue(AddFeatureValueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var id = await _productService.AddFeatureValue(model);
            await _loggerService.AddLog(id, User.GetUserId(), "افزودن مقدار ویژگی");
            return Redirect("/Admin/Feature");

        }

        [Route("EditFeatureValue/{id}")]
        public async Task<IActionResult> EditFeatureValue(int id)
        {
            var featureValue = await _productService.GetFeatureValueByIdForEdit(id);
            return View(featureValue);
        }

        [Route("EditFeatureValue/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditFeatureValue(EditFeatureValueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var status = await _productService.UpdateFeatureValue(model);
            await _loggerService.AddLog(model.Id, User.GetUserId(), "ویرایش مقدار ویژگی");
            return Redirect("/Admin/Feature");

        }

        [Route("DeleteFeatureValue/{id}")]
        public async Task<IActionResult> DeleteFeatureValue(int id)
        {
            var featureValue = await _productService.GetFeatureValueByIdForEdit(id);
            return View(featureValue);
        }


        [Route("DeleteFeatureValue/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteFeatureValue(EditFeatureValueViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var id = await _productService.DeleteFeatureValue(model);
            if (id!=true)
            {
                return BadRequest();
            }
            await _loggerService.AddLog(model.Id, User.GetUserId(), "حذف مقدار ویژگی");
            return Redirect("/Admin/Values/" +model.FeatureId );
        }
    }
}
