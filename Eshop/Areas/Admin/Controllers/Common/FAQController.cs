using Application.Interface;
using Application.Security;
using Domain.ViewModels.FAQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace Eshop.Areas.Admin.Controllers.Common
{
    public class FAQController : AdminBaseController
    {
        #region Injections
        private IFaqService _faqService;
        private ILoggerService _loggerService;

        public FAQController(IFaqService faqService, ILoggerService loggerService)
        {
            _faqService = faqService;
            _loggerService = loggerService;
        }

        #endregion


        [Route("FAQs")]
        public async Task<IActionResult> FAQs(FilterFaqViewModel filter)
        {
            var model = await _faqService.GetAllFaqsForAdmin(filter);
            return View(model);
        }


        #region Add

        [Route("AddFaq")]
        public IActionResult AddFaq()
        {
            return View();
        }

        [Route("AddFaq")]
        [HttpPost]
        public async Task<IActionResult> AddFaq(AddOrEditFaqViewModel faq)
        {
            if (!ModelState.IsValid)
                return View(faq);
            var res = await _faqService.AddFaqFromAdmin(faq);
            await _loggerService.AddLog(res, User.GetUserId(), "افزودن پرسش پر تکرار");
            return RedirectToAction("FAQs");
        }

        #endregion

        #region Delete

        [Route("DeleteFaq/{id}")]
        public async Task<IActionResult> DeleteFaq(int id)
        {
            var model = await _faqService.GetFaqById(id);
            return View("FaqManager", model);
        }

        [Route("DeleteFaq/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteFaq(AddOrEditFaqViewModel faq)
        {
            if (!ModelState.IsValid)
                return View("FaqManager", faq);

            var delete =await _faqService.DeleteFaqFromAdmin((int)faq.Id);
            await _loggerService.AddLog((int)faq.Id, User.GetUserId(), "حذف پرسش پر تکرار");
            return RedirectToAction("FAQs");
        }

        #endregion


        #region Edit


        [Route("EditFaq/{id}")]
        public async Task<IActionResult> EditFaq(int id)
        {
            ViewData["Edit"] = true;
            var model = await _faqService.GetFaqById(id);
            return View("FaqManager",model);
        }

        [Route("EditFaq/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditFaq(AddOrEditFaqViewModel faq)
        {
            if (!ModelState.IsValid)
                return View("FaqManager", faq);

            var res = await _faqService.EditFaqFromAdmin(faq);
            await _loggerService.AddLog((int)faq.Id, User.GetUserId(), "ویرایش پرسش پر تکرار");
            return RedirectToAction("FAQs");
        }

        #endregion

    }
}
