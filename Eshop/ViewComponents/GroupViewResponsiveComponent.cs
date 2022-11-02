using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.ViewComponents
{
    public class GroupViewResponsiveComponent:ViewComponent
    {
        private IProductService _productService;

        public GroupViewResponsiveComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult((IViewComponentResult)View("_GroupViewResponsiveComponent"));

        }

    }
}
