using Application.Interface;
using Eshop.Controllers.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : BaseSiteController
    {
        private IProductService _productService;

        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }
        [Produces("application/json")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search()
        {

            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var res = await _productService.GetProductTitleForSearch(term);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
