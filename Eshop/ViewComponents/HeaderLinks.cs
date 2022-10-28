using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.ViewComponents
{
    public class HeaderLinks : ViewComponent
    {
        private IDynamicLinkService _linkService;

        public HeaderLinks(IDynamicLinkService linkService)
        {
            _linkService = linkService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _linkService.GetLinksForHeader();
            return await Task.FromResult((IViewComponentResult)View("_HeaderLinks", model));
        }


    }
}
