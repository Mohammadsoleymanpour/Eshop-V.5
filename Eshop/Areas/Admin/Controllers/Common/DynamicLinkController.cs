using Application.Interface;
using Application.StaticTools;
using Domain.ViewModels.DynamicLinks;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using Eshop.Common;

namespace Eshop.Areas.Admin.Controllers.Common
{
    public class DynamicLinkController : AdminBaseController
    {
        private IDynamicLinkService _dynamicLinkService;
        private ILoggerService _loggerService;

        public DynamicLinkController(IDynamicLinkService dynamicLinkService, ILoggerService loggerService)
        {
            _dynamicLinkService = dynamicLinkService;
            _loggerService = loggerService;
        }

        [CheckPermission(Permissions.LinkManagement)]
        [Route("Links")]
        public async Task<IActionResult> Index(LinksForAdminViewModel filter)
        {
            var res = await _dynamicLinkService.GetLinksForAdmin(filter);

            return View(res);
        }

        #region Add
        [CheckPermission(Permissions.AddLink)]
        [Route("AddLink")]
        public async Task<IActionResult> AddLink()
        {
            return View();
        }


        [Route("AddLink")]
        [HttpPost]
        public async Task<IActionResult> AddLink(AddLinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var addlink = await _dynamicLinkService.AddLink(model);
            await _loggerService.AddLog(addlink, User.GetUserId(), "افزودن لینک مفید");
            return Redirect("/Admin/Links");
        }

        #endregion

        #region Edit
        [Route("EditLink/{id}")]
        [CheckPermission(Permissions.EditLink)]
        public async Task<IActionResult> EditLink(int id)
        {
            var res = await _dynamicLinkService.GetViewModelLinkById(id);
            res.Id = id;

            return View(res);
        }

        [Route("EditLink/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditLink(EditLinkViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }


            var status = await _dynamicLinkService.Updatelink(model);

            if (status != true)
            {
                return BadRequest();
            }
            await _loggerService.AddLog(model.Id, User.GetUserId(), "ویرایش لینک مفید");
            return Redirect("/Admin/Links");
        }

        #endregion

        #region Delete
        [Route("DeleteLink/{id}")]
        [CheckPermission(Permissions.DeleteLink)]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var res = await _dynamicLinkService.GetLinkById(id);

            if (!await _dynamicLinkService.Deletelink(res.Id))
            {
                return BadRequest();
            }
            else
            {
                await _loggerService.AddLog(id, User.GetUserId(), "ویرایش بنر");
                return Redirect("/Admin/Links");
            }
        }

        #endregion
    }
}
