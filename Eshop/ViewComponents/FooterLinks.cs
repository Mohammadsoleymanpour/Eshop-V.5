using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Eshop.ViewComponents
{
    public class FooterLinks : ViewComponent
    {
        private IDynamicLinkService _linkService;

        public FooterLinks(IDynamicLinkService linkService)
        {
            _linkService = linkService;
        }


      
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _linkService.GetLinksForFooter();
            return await Task.FromResult((IViewComponentResult)View("_FooterLinks", model));
        }

    }
}
