using Application.Interface;
using Application.Security;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Eshop.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("Authrize")]
    public class AuthrizeTagHelper : TagHelper
    {
        private IPermissionService _permissionService;

        public AuthrizeTagHelper(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public int PermissionId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // var user  = var user = actionAccessor.ActionContext.HttpContext.User;
            var uid = ViewContext.HttpContext.User.GetUserId();


            
            if (await _permissionService.CheckPermission(PermissionId,uid))
            {
              
            }
            else
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("style", "display: none");
                output.SuppressOutput();
            }

           
        }
    }   
}
