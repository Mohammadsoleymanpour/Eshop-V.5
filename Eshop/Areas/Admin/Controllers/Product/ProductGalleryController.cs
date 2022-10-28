using Application.Interface;
using Application.Security;
using Domain.ViewModels.Product;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers.Product
{
    [CheckPermission(Permissions.ProductGallery)]
    public class ProductGalleryController : AdminBaseController
    {
        private IProductService _productService;
        private ILoggerService _loggerService;

        public ProductGalleryController(IProductService productService, ILoggerService loggerService)
        {
            _productService = productService;
            _loggerService = loggerService;
        }

        [Route("ProductGallery/{id}")]
        public IActionResult Index(int id)
        {
            ViewBag.productId = id;
            var galleries = _productService.GetAllImageGalleryFromAdmin(id);
            return View(galleries);
        }

        #region Add


        [Route("AddImageProduct/{id}")]
        public IActionResult AddImage(int id)
        {
            var model = new AddImageGalleryViewModel()
            {
                ProductId = id
            };
            return View(model);
        }

        [Route("AddImageProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddImage(AddImageGalleryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int id = await _productService.AddImageFromAdmin(model);
            await _loggerService.AddLog(id, User.GetUserId(), "افزودن عکس به محصول");
            return Redirect("/Admin/ProductGallery/" + model.ProductId);
        }

        #endregion

        #region Edit

        [Route("EditImage/{id}")]
        public async Task<IActionResult> EditImage(int id)
        {
            var image = await _productService.GetGalleryById(id);
            var editViewModel = new EditImageGalleryViewModel()
            {
                Id = image.Id,
                imageName = image.ImageName,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
            };
            return View(editViewModel);
        }

        [Route("EditImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditImage(EditImageGalleryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var image = await _productService.GetGalleryById(model.Id);
           var res=await _productService.DeleteImage(image);
           int id = await _productService.AddImageFromAdmin(model);
            await _loggerService.AddLog(id, User.GetUserId(), "ویرایش عکس محصول");
            return Redirect("/Admin/ProductGallery/" + model.ProductId);
        }

        #endregion

        #region Delete
        [Route("DeleteImage/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var Gallery=await _productService.GetGalleryById(id);
            var editViewModel = new EditImageGalleryViewModel()
            {
                Id = Gallery.Id,
                imageName = Gallery.ImageName,
                IsDefault = Gallery.IsDefault,
                ProductId = Gallery.ProductId,
            };
            return View(editViewModel);
        }

        [Route("DeleteImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(EditImageGalleryViewModel model)
        {
            var Gallery = await _productService.GetGalleryById(model.Id);
            var prId = Gallery.ProductId;
            if (Gallery==null)
            {
                return NotFound();
            }
            bool res=await _productService.DeleteImage(Gallery);
           if (!res)
           {
               return NotFound();
           }
            await _loggerService.AddLog(model.Id, User.GetUserId(), "حذف عکس از محصول");
            return Redirect("/Admin/ProductGallery/"+prId);
        }

        #endregion
    }
}
