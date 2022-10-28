using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.ViewComponents
{
    public class DynamicPagesViewComponent:ViewComponent
    {
        private readonly IDynamicPageService _pageService;

        public DynamicPagesViewComponent(IDynamicPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _pageService.GetAllPagesForSite();
            return await Task.FromResult((IViewComponentResult)View("_DynamicPages", model));
        }
    }
}
