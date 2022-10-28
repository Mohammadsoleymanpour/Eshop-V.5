using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Tickets;
using Domain.ViewModels.Ticket;
using Domain.ViewModels.User;

namespace Application.Interface
{
    public interface ITicketService
    {
        /// <summary>
        /// Get A List Of Ticket For User Using A ViewModel For Filter And Paging
        /// </summary>
        /// <param name="filter">A View Model For Search Filtet And Paging</param>
        /// <returns>A View Model With Paging</returns>
        Task<FilterTicketListViewModel> GetListTicketsForUser(FilterTicketListViewModel filter);

        /// <summary>
        /// Get All Ticket That Unseen By Admin
        /// </summary>
        /// <param name="filter">A View Model For Search Filtet And Paging</param>
        /// <returns>A View Model With Paging</returns>
        Task<UserTicketsListViewModel> GetNoReadTickets(UserTicketsListViewModel filter);

        /// <summary>
        /// Get A List Of Ticket For Admin Using A ViewModel For Filter And Paging
        /// </summary>
        /// <param name="filter">A View Model For Search Filtet And Paging</param>
        /// <returns>A View Model With Paging</returns>
        Task<UserTicketsListViewModel> GetListTicketsForAdmin(UserTicketsListViewModel filter);

        /// <summary>
        /// This Method Add A Ticket To User From Admin
        /// </summary>
        /// <param name="ticket">A View Model That Has All Fields Needed To Add A New Ticket</param>
        /// <returns>The Newly Saved Method</returns>
        Task<Ticket> AddTicketFromAdmin(AddTicketFromAdminViewModel ticket);

        /// <summary>
        /// Get All User For Admin To Send A Ticket
        /// </summary>
        /// <returns>List Of ViewModel That Have All Required Fields</returns>
        Task<List<GetUserForAddTicketFromAdminViewModel>> GetUser();

        /// <summary>
        /// Get List Messages Of A Ticket For Admin
        /// </summary>
        /// <param name="ticketId">Ticket Id That Is Needed</param>
        /// <returns>List Of Model Ticket Messages</returns>
        Task<List<TicketMassages>> GetTicketMassagesForAdmin(int ticketId);

        /// <summary>
        /// Add New Message To Ticket From Admin
        /// </summary>
        /// <param name="massages">A Model Of Ticket Message</param>
        /// <returns>The Newly Added Message Model</returns>
        Task<TicketMassages> AddTicketMassageFromAdmin(TicketMassages massages);

        /// <summary>
        /// Get A Ticket By Id
        /// </summary>
        /// <param name="ticketId"> ID Of Needed Ticket</param>
        /// <returns>A Ticket Model</returns>
        Ticket GetTicketById(int ticketId);

        /// <summary>
        /// Add A Ticket Form User To DataBase
        /// </summary>
        /// <param name="userId">User Id That Is Adding The Ticket</param>
        /// <param name="ticket">A View Model That Has All Fields Needed</param>
        /// <returns> A Enum That Indicate The Result Of Opration</returns>
        Task<Enum> AddTicketFromUser(int userId, AddTicketFromUserViewModel ticket);

        /// <summary>
        /// This Method Get All Details And Messages Of A Ticket For User
        /// </summary>
        /// <param name="ticketId"> Ticket Id Of The Needed Ticket</param>
        /// <returns>A View Model Of All Needed Method</returns>
        Task<DetailTicketForUserViewModel> GetDetailTicketForUser(int ticketId);

        /// <summary>
        /// This Method Add a Message To Ticket From Admin
        /// </summary>
        /// <param name="message">Text Of Message</param>
        /// <param name="senderId">Id Of The Current Message Sender (User) </param>
        /// <param name="ticketId">The Id Of The Ticket That Message Is Adding To</param>
        /// <returns></returns>
        Task<bool> AddMessage(string message,int senderId, int ticketId);

        /// <summary>
        /// This Change Status Of Ticket That Is Read By Admin
        /// </summary>
        /// <param name="ticketId">Id Of Ticket</param>
        /// <returns>The Current Ticket</returns>
        Ticket ReadTicketFromAdmin(int ticketId);

        /// <summary>
        /// A Method For Closing A Ticket From Admin
        /// </summary>
        /// <param name="ticketId">Id Of Ticket Is Going To Close</param>
        /// <returns>A Bool That Indicate The Result</returns>
        bool CloseTicketFromAdmin(int ticketId);
    }
}
