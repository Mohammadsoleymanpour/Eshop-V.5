using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;
//using RedirectResult = System.Web.Mvc.RedirectResult;

namespace Application.Security
{
    public class CheckPermission : AuthorizeAttribute, IAuthorizationFilter
    {
        IPermissionService _permissionService;
        private int _permissionId = 0;

        public CheckPermission(int permissionId)
        {
            _permissionId = permissionId;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {


                if (!await _permissionService.CheckPermission( _permissionId, context.HttpContext.User.GetUserId()))
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("/Login");
            }
        }
    }
}
