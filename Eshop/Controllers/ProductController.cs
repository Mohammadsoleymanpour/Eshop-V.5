using Application.Interface;
using Application.Security;
using Domain.Models.Product;
using Domain.ViewModels.Product;
using Eshop.Controllers.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ProductController : BaseSiteController
    {
        #region Injection

        private IProductService _productService;
        IUserService _userService;
        private IVoteService _voteService;

        public ProductController(IProductService productService, IUserService userService, IVoteService voteService)
        {
            _productService = productService;
            _userService = userService;
            _voteService = voteService;
        }

        #endregion
        
        [Route("Product/{id}")]
        public async Task<IActionResult> Index(int id, bool comment = false)
        {
            var product = await _productService.GetProductByIdForDetail(id);
            var r = 
             ViewData["Features"]  =await _productService.GetAllFeaturesSelected(id);
             var res = await _productService.GetAllPricesOfProduct(id);
             ViewData["productPrice"] = res;
             ViewData["FeaturevValues"] = await _productService.GetAllFeatureValues();
             ViewData["Prices"] =await _productService.GetAllPricesOfProduct(id);
                var similar= await _productService.GetSimilarProduct(id);
                ViewData["Similar"] = similar;
            if (comment == true)
            {
                ViewBag.Comment = true;
            }
            return View(product);
        }
        [Authorize]
        [Route("AddComment/{id}")]
        public async Task<IActionResult> AddComment(int id, int? parentId)
        {
            string img = await _productService.GetDefaultImageById(id);
            ViewBag.DefaultImage = img;
            var product = await _productService.GetProductByIdForDetail(id);
            ViewData["product"] = product;
            return View(new AddCommentViewModel { ProductId = id, ParentId = parentId });
        }

        [Route("AddComment/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            model.SenderId = User.GetUserId();
            if (!ModelState.IsValid)
            {
                return View();
            }

            int id = await _productService.AddCommentFromUser(model);
            return Redirect("/Product/" + model.ProductId + "?comment=true");
        }
        [Authorize]
        [Route("AddFavoriteProduct/{id}")]
        public async Task<IActionResult> AddFavoriteProduct(int id)
        {
            int UserId = User.GetUserId();
            var res = await _productService.AddFavoriteProduct(id, UserId);

            return Json(res);
        }

        [Route("FilterProduct/{id}")]
        public async Task<IActionResult> ProductFilter(FilterProductByCategory model, int id)
        {
            var res = await _productService.GetProductByCategorty(model, id);
            return View(res);
        }

        [Route("FilterProductByName")]
        public async Task<IActionResult> ProductFilter(FilterProductByCategory model, string name)
        {
            var res = await _productService.GetProductByCategortyName(model, name);
            return View(res);
        }
        [Authorize]
        [Route("AddVote/{productId}")]
        public async Task<IActionResult> AddVote(int productId, bool vote)
        {
            var res = await _voteService.AddProductVote(productId, User.GetUserId(), vote);
            return Redirect("/product/"+productId);
        }
        [Authorize]
        [Route("AddCommentVote/{commentId}")]
        public async Task<IActionResult> AddCommentVote(int commentId, bool vote)
        {
            var res = await _voteService.AddCommentVote(commentId, User.GetUserId(), vote);
            var product = _voteService.GetProductByCommentId(commentId);
            return Redirect("/product/" + product.Id);
        }


        [Route("AllProducts")]
        public async Task<IActionResult> AllProducts(FilterProductByCategory filter)
        {
            var products =await _productService.GetAllProductsCategoryAsync();
            return View("ProductFilter");
        }
        [Route("Products")]
        public async Task<IActionResult> Products(FilterProductByCategory filter)
        {
            var products = await _productService.GetAllProducts(filter);
            return View("ProductFilter", products);
        }
    }
}
