using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Enums;
using Domain.Models.Tickets;
using Domain.Models.Tickets;
using Domain.ViewModels.Ticket;
using Domain.ViewModels.User;

namespace Application.Services
{
    public class TicketServices : ITicketService
    {
        #region Injections

        private ITicketRepository _ticketRepository;

        public TicketServices(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        #endregion

        public async Task<FilterTicketListViewModel> GetListTicketsForUser(FilterTicketListViewModel filter)
        {
            return await _ticketRepository.GetAllTicketsForUser(filter);
        }

        public Task<UserTicketsListViewModel> GetNoReadTickets(UserTicketsListViewModel filter)
        {
            return _ticketRepository.GetNoReadTickets(filter);
        }

        public async Task<UserTicketsListViewModel> GetListTicketsForAdmin(UserTicketsListViewModel filter)
        {
            return await _ticketRepository.GetAllTicketsForUser(filter);
        }

        public Task<Ticket> AddTicketFromAdmin(AddTicketFromAdminViewModel ticket)
        {

            var addticket = new Ticket()
            {
                 Status = TicketStatusEnum.Answered,
                 CreatDate = DateTime.Now,
                 IsDelete = false,
                 IsReadByAdmin = true,
                 IsReadByOwner = false,
                 Levels = ticket.Levels,
                 Part = ticket.Parts,
                 Title = ticket.Title,
                 OwnerId = ticket.OwnerId,
                 
            };
            return _ticketRepository.AddTicketFromAdmin(addticket);

        }

        public Task<List<GetUserForAddTicketFromAdminViewModel>> GetUser()
        {
           return _ticketRepository.GetUserForTicket();
        }

        public Task<List<TicketMassages>> GetTicketMassagesForAdmin(int ticketId)
        {
            return _ticketRepository.GetTicketMassagesForAdmin(ticketId);
        }

        public Task<TicketMassages> AddTicketMassageFromAdmin(TicketMassages massages)
        {
            return _ticketRepository.AddTicketMassageFromAdmin(massages);

        }

        public Ticket GetTicketById(int ticketId)
        {
            return _ticketRepository.geTicketById(ticketId);
        }

        public async Task<Enum> AddTicketFromUser(int userId, AddTicketFromUserViewModel ticket)
        {
            try
            {
                int id = await _ticketRepository.AddTicket(userId, ticket);
            }
            catch
            {
                return TicketEnums.AddTicket.Failed;
            }



            return TicketEnums.AddTicket.Success;
        }

        public async Task<DetailTicketForUserViewModel> GetDetailTicketForUser(int ticketId)
        {
            var res = new DetailTicketForUserViewModel()
            {
                Messages = await _ticketRepository.GetMessagesByTicketId(ticketId),
                Ticket = await _ticketRepository.GetTicketForById(ticketId)
            };

            return res;
        }

        public async Task<bool> AddMessage(string message, int senderId, int ticketId)
        {
            var ticket = await _ticketRepository.GetTicketForById(ticketId);
            var mess = new TicketMassages()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                Message = message,
                SenderId = senderId,
                TicketId = ticketId,
                Ticket = ticket,
                
            };
            try
            
            
            {
                ticket.TicketMassagesList.Add(mess);
                _ticketRepository.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Ticket ReadTicketFromAdmin(int ticketId)
        {
            var getticket = _ticketRepository.geTicketById(ticketId);
            getticket.IsReadByAdmin=true;
            getticket.Status = TicketStatusEnum.Answered;
            _ticketRepository.Save();
            return getticket;
        }

        public bool CloseTicketFromAdmin(int ticketId)
        {
            var ticket=_ticketRepository.geTicketById(ticketId);
            if (ticket==null)
            {
                return false;   
            }
            ticket.Status=TicketStatusEnum.Closed;
            _ticketRepository.Save();
            return true;
        }
    }
}
