using Application.Interface;
using Application.Sender;
using Domain.ViewModels.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using NuGet.Protocol.Plugins;
using Application.Security;
using Eshop.Common;
using Application.Services;
using Application.StaticTools;

namespace Eshop.Areas.Admin.Controllers.ContactUs
{


    public class ContactUsController : AdminBaseController
    {
        #region Injection

        private IContactUssService _ContactUssService;
        private ILoggerService _loggerService;

        public ContactUsController(IContactUssService contactUssService, ILoggerService loggerService)
        {
            _ContactUssService = contactUssService;
            _loggerService = loggerService;
        }


        #endregion

        #region Get ALl Contact Us Tickets
        [CheckPermission(Permissions.ContactUsManagement)]
        [Route("GetContactUsss")]
        public async Task<IActionResult> GetContactUsss(FilterContactUssViewModel filter)
        {
            var list = await _ContactUssService.GetContactUsss(filter);

            return View(list);
        }

        #endregion

        #region Answer

        [CheckPermission(Permissions.AnswerContactUs)]
        [Route("AnswerContactUs/{id}")]

        public IActionResult AnswerContactUs(int id)
        {
            var info = _ContactUssService.GetContactById(id);
            if (info == null)
                return RedirectToAction("GetContactUsss");

            return View(info);
        }


        [Route("AnswerContactUs/{id}")]
        [HttpPost]
        public async Task<IActionResult> AnswerContactUs(AnswerContactUsViewModel answer)
        {

            try
            {
                SendEmail.Send(answer.Email, $"پاسخ به تماس شما در دسته {answer.Subject}", answer.AnswerBody.BuildView());

            }
            catch
            {
                return NotFound();
            }

            _ContactUssService.AnswerContactUsTicket(answer.Id, answer.AnswerBody);
            await _loggerService.AddLog(answer.Id, User.GetUserId(), " پاسخ به تماس با ما");

            return RedirectToAction("GetContactUsss");
        }

        #endregion
    }
}
