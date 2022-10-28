using Application.Interface;
using Domain.Models.Enums;
using Domain.ViewModels.SocialMedia;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using Eshop.Common;

namespace Eshop.Areas.Admin.Controllers.Common
{
    public class SocialMediaController : AdminBaseController
    {
        #region Injections

        private ISocialMediaService _socialMediaService;
        private ILoggerService _loggerService;

        public SocialMediaController(ISocialMediaService socialMediaService, ILoggerService loggerService)
        {
            _socialMediaService = socialMediaService;
            _loggerService = loggerService;
        }



        #endregion

        [Route("SocialMedia")]
        [CheckPermission(Permissions.LinkManagement)]
        public async Task<IActionResult> SocialMedia(FilterSocialMediaForAdminViewModel filter)
        {
            var model = await _socialMediaService.GetAllMediasForAdmin(filter);
            return View(filter);
        }

        [Route("DeleteSocialMedia/{id}")]
        [CheckPermission(Permissions.DeleteLink)]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var res = await _socialMediaService.DeleteSocial(id);
            await _loggerService.AddLog(id, User.GetUserId(), "حذف شبکه اجتماعی");
            if (!res)
            {
                ViewBag.IsSuccess = false;
                return RedirectToAction("SocialMedia");
            }
            return RedirectToAction("SocialMedia");
        }

        [CheckPermission(Permissions.EditLink)]
        [Route("EditSocialMedia/{id}")]
        public async Task<IActionResult> EditSocialMedia(int id)
        {
            ViewBag.IsEditing = true;
            var model = await _socialMediaService.GetMediaById(id);
            return View(model);
        }


        [Route("EditSocialMedia/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditSocialMedia(EditSocialMediaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if(await _socialMediaService.IsMediaAlreadyHasUrl(model.Platform))
            {
                ModelState.AddModelError("Platform", "این پلتفرم در حال حاضر لینک دارد.");
                return View(model);
            }
            var result = await _socialMediaService.EditSocialMediaLink(model);
            await _loggerService.AddLog(model.Id, User.GetUserId(), "ویرایش شبکه اجتماعی");
            return RedirectToAction("SocialMedia");
        }

        [Route("AddSocialMedia")]
        [CheckPermission(Permissions.AddLink)]
        public async Task<IActionResult> AddSocialMedia()
        {
            return View();
        }

        [Route("AddSocialMedia")]
        [HttpPost]
        public async Task<IActionResult> AddSocialMedia(AddSocialMediaLinkViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (await _socialMediaService.IsMediaAlreadyHasUrl((SocialMediaPlatform) model.PlatForm))
            {
                ModelState.AddModelError("PlatForm", "این پلتفرم در حال حاضر لینک دارد.");
               
                return View(model);
            }
            var res  =await _socialMediaService.AddSocialMedia(model);
            await _loggerService.AddLog(res, User.GetUserId(), "افزودن شبکه اجتماعی");
            return RedirectToAction("SocialMedia");
        }
    }
}
