using Application.Interface;
using Domain.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using Eshop.Common;

namespace Eshop.Areas.Admin.Controllers.Product
{
    [CheckPermission(Permissions.UserFavorites)]
    public class FavoriteProductController : AdminBaseController
    {
        private IProductService _productService;

        public FavoriteProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("FavoriteProduct/{id}")]
        [CheckPermission(Permissions.UserManagement)]
        public async Task<IActionResult> Index(FavoriteProductViewModel model,int id)
        {
            return View( await _productService.GetFavoriteProductForAdmin(model,id));
        }
    }
}
