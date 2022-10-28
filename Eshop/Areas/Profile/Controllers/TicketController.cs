using Application.Enums;
using Application.Interface;
using Application.Security;
using Domain.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Profile.Controllers
{

    public class TicketController : BaseProfileController
    {
        #region Injections

        private ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        #endregion
        

        [Route("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets(FilterTicketListViewModel filter)
        {
            var list = await _ticketService.GetListTicketsForUser(filter);

            return View(list);
        }

        [Route("NewTicket")]
        public IActionResult NewTicket() => View();

        [HttpPost("NewTicket")]
        public async Task<IActionResult> NewTicket(AddTicketFromUserViewModel ticket)
        {
            if (!ModelState.IsValid)
                return View(ticket);
            var res = await _ticketService.AddTicketFromUser(User.GetUserId(), ticket);


            return RedirectToAction("GetAllTickets");
        }

        [Route("DetailTicket/{id}")]
        public async Task<IActionResult> DetailTicket(int id)
        {
            var model = await _ticketService.GetDetailTicketForUser(id);
            return View(model);
        }


        [HttpPost("DetailTicket/{id}")]
        public async Task<IActionResult> DetailTicket(string replyMessage, int ticketId)
        {
            if (string.IsNullOrEmpty(replyMessage))
            {
                ModelState.AddModelError("replyMessage", "لطفا پیام را بنویسید");
                return View();
            }

            var res = await _ticketService.AddMessage(replyMessage, User.GetUserId(), ticketId);

            return Redirect("/Profile/DetailTicket/" + ticketId);

        }
    }
}