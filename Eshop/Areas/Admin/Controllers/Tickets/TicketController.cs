using Application.Interface;
using Application.Security;
using Domain.Models.Enums;
using Domain.Models.Tickets;
using Domain.ViewModels.Ticket;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eshop.Areas.Admin.Controllers.Tickets
{
    public class TicketController : AdminBaseController
    {
        private ITicketService _ticketService;
        private ILoggerService _loggerService;

        public TicketController(ITicketService ticketService, ILoggerService loggerService)
        {
            _ticketService = ticketService;
            _loggerService = loggerService;
        }
        [Route("Ticket")]
        [CheckPermission(Permissions.TicketManagement)]
        public async Task<IActionResult> Index(UserTicketsListViewModel filter)
        {
            var query = await _ticketService.GetListTicketsForAdmin(filter);

            return View(query);
        }

        [Route("AddTicket")]
        [CheckPermission(Permissions.AddTicket)]
        public async Task<IActionResult> AddTicket()
        {
            var user = await _ticketService.GetUser();
            ViewBag.UserList = new SelectList(user, "Id", "Email");


            return View();
        }

        [Route("AddTicket")]
        [HttpPost]
        public async Task<IActionResult> AddTicket(AddTicketFromAdminViewModel ticket)
        {
            if (!ModelState.IsValid)
            {

                return View(ticket);
            }


            var query = await _ticketService.AddTicketFromAdmin(ticket);

            if (query == null)
            {
                ViewBag.Error = true;
                return View(ticket);
            }

            await _loggerService.AddLog(query.Id, User.GetUserId(), "افزودن تیکت");
            return Redirect("/Admin/Ticket");
        }

        [Route("TicketMassage/{id}")]
        [CheckPermission(Permissions.AnswerTicket)]
        public async Task<IActionResult> TicketMassage(int id)
        {

            var ticket = _ticketService.ReadTicketFromAdmin(id);

            ViewData["Ticket"] = ticket;
            ViewData["Massage"] = await _ticketService.GetTicketMassagesForAdmin(id);
            return View();
        }




        [Route("AddTicketMassage")]
        [HttpPost]
        [CheckPermission(Permissions.AnswerTicket)]
        public async Task<IActionResult> AddTicketMassage(int ticketId, string tickemassage)
        {

            var ticket = new TicketMassages()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                Message = tickemassage,
                SenderId = User.GetUserId(),
                TicketId = ticketId,

            };


            if (string.IsNullOrEmpty(ticket.Message))
            {

                return Redirect("/Admin/Ticket");
            }


            var query = await _ticketService.AddTicketMassageFromAdmin(ticket);

            if (query == null)
            {
                ViewBag.Error = true;
                return Redirect("/TicketMassage/" + query.TicketId);
            }
            await _loggerService.AddLog(query.Id, User.GetUserId(), "افزودن پیام به تیکت");
            return Redirect("/TicketMassage/" + query.TicketId);

        }
        [Route("CloseTicket/{id}")]
        [CheckPermission(Permissions.CloseTicket)]
        public async Task<IActionResult> CloseTicket(int id)
        {
            if (!_ticketService.CloseTicketFromAdmin(id))
            {
                return NotFound();
            }
            await _loggerService.AddLog(id, User.GetUserId(), "بستن تیکت ");
            return Redirect("/Admin/Ticket");
        }
    }
}
