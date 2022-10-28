using Application.Interface;
using Application.Security;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Domain.ViewModels.ContactUs;
using Eshop.Controllers.Shared;

namespace Eshop.Controllers
{
    public class ContactUssController : BaseSiteController
    {
        private IContactUssService _ContactUssService;

        public ContactUssController(IContactUssService ContactUssService)
        {
            _ContactUssService = ContactUssService;
        }
        [Route("ContactUss")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("ContactUss")]
        [HttpPost]
        public IActionResult Index(ContactUssViewModel ContactUss)
        {
            if (!ModelState.IsValid)
            {
                return View(ContactUss);
            }
            ContactUss.Ip = HttpContext.GetIp();
            var addContactUss = _ContactUssService.AddContactUssFromUser(ContactUss);
           ViewBag.IsSuccess=true;
            return View();
        }
    }
}
