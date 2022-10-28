using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using Application.Interface;
using Application.Sender;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.User;


namespace Eshop.ViewComponents
{
    public class SocialMediaVC:ViewComponent
    {
        private ISocialMediaService _socialMediaService;

        public SocialMediaVC(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res =await _socialMediaService.GeMediaForShow();

            return await Task.FromResult((IViewComponentResult)View("SocialMediaVC",res));
        }
    }
}
