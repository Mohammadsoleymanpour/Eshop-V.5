using Application.Interface;
using Application.Security;
using Domain.ViewModels.Banner;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers.Common
{
    public class BannerController : AdminBaseController
    {
        private IBannerService _bannerService;
        private ILoggerService _loggerService;

        public BannerController(IBannerService bannerService, ILoggerService loggerService)
        {
            _bannerService = bannerService;
            _loggerService = loggerService;
        }


        [Route("Banners")]
        public async Task<IActionResult> Banners(FilterBannerViewModel filter)
        {
            var model = await _bannerService.GetAllBannersForAdmin(filter);
            return View(model);
        }

        #region Edit

        [Route("EditBanner/{id}")]
        public async Task<IActionResult> EditBanner(int id)
        {
            var model = await _bannerService.GetBannerById(id);
            ViewBag.Edit = true;
            return View("BannerManger",model);
        }

        [Route("EditBanner/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditBanner(AddOrEditBannerViewModel model)
        {
            if (!ModelState.IsValid)
                return View("BannerManger", model);
            if (await _bannerService.IsBannerExist(model.Position))
            {
                ModelState.AddModelError("Position", " بنر دارد. لطفا ویرایش کنید");
                return View("BannerManger", model);
            }
            var res =await _bannerService.EditBannerFromAdmin(model);
           var res2= await _loggerService.AddLog((int)model.Id,User.GetUserId(), "ویرایش بنر");
            return RedirectToAction("Banners");
        }

        #endregion

        #region Add
        [Route("AddBanner")]
        public IActionResult AddBanner()
        {
            ViewBag.AddBanner = true;
            return View("BannerManger"); 
        }


        [Route("AddBanner")]
        [HttpPost]
        public async Task<IActionResult> AddBanner(AddOrEditBannerViewModel model)
        {
            if (!ModelState.IsValid)
                return View("BannerManger",model);
            if (await _bannerService.IsBannerExist(model.Position))
            {
                ModelState.AddModelError("Position", "این موقعیت بنر دارد. لطفا ویرایش کنید");
                return View("BannerManger", model);
            }
            var res = await _bannerService.AddBannerFromAdmin(model);
            if (res==0)
            {
                return BadRequest();
            }
             await _loggerService.AddLog(res, User.GetUserId(), "افزودن بنر");
            return RedirectToAction("Banners");
        }

        #endregion

        #region Delete

        [Route("DeleteBanner/{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var model = await _bannerService.GetBannerById(id);
            ViewBag.Delete = true;
            return View("BannerManger",model);
        }

        [Route("DeleteBanner/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteBanner(AddOrEditBannerViewModel model)
        {
            var res = await _bannerService.DeleteBannerFromAdmin((int)model.Id);
            await _loggerService.AddLog((int)model.Id, User.GetUserId(), "حذف بنر");
            return RedirectToAction("Banners");
        }

        #endregion
    }
}
