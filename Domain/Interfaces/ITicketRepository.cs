using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Tickets;
using Domain.ViewModels.Ticket;
using Domain.ViewModels.User;

namespace Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<int> AddTicket(int userId, AddTicketFromUserViewModel ticket);
       
        Task<Ticket> GetByOwnerId(int id);
        Task<Ticket> CloseTicket(int ticketId);
        Task<Ticket> AnwerTicket(int ticketId);
        Task<FilterTicketListViewModel> GetAllTicketsForUser(FilterTicketListViewModel filter);
        Task<UserTicketsListViewModel> GetNoReadTickets(UserTicketsListViewModel filter);
        Task<UserTicketsListViewModel> GetAllTicketsForUser(UserTicketsListViewModel filter);

        Task<Ticket> AddTicketFromAdmin(Ticket ticket);

        Task<List<GetUserForAddTicketFromAdminViewModel>> GetUserForTicket();
        Task<List<TicketMassages>> GetTicketMassagesForAdmin(int ticketId);
        Task<TicketMassages> AddTicketMassageFromAdmin(TicketMassages massages);
        Ticket geTicketById(int ticketId);
        Task<Ticket> GetTicketForById(int ticketId);
        Task<List<TicketMassages>> GetMessagesByTicketId(int ticketId);
         
        void Save();
    }
}
