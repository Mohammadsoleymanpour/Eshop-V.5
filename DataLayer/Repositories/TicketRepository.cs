using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Enums;
using Domain.Models.Tickets;
using Domain.ViewModels.Ticket;
using Domain.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        #region Injections

        private ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion


        public async Task<int> AddTicket(int userId, AddTicketFromUserViewModel ticket)
        {
            var addTicket = new Ticket()
            {
                OwnerId = userId,
                CreatDate = DateTime.Now,
                IsDelete = false,
                IsReadByAdmin = false,
                IsReadByOwner = true,
                Levels = ticket.TicketLevels,
                Status = TicketStatusEnum.Pending,
                Part = ticket.TicketParts,
                TicketMassagesList = new List<TicketMassages>(){new TicketMassages()
                {
                    CreatDate = DateTime.Now,
                    Message = ticket.Message,
                    SenderId = userId,
                    IsDelete = false
                }},
                Title = ticket.Subject
            };
            _context.Add(addTicket);
            await _context.SaveChangesAsync();
            return addTicket.Id;
        }



        public Task<Ticket> GetByOwnerId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> CloseTicket(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> AnwerTicket(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<FilterTicketListViewModel> GetAllTicketsForUser(FilterTicketListViewModel filter)
        {
            var query = _context.Tickets
                .Include(t => t.TicketMassagesList)
                .Include(c => c.User)
                .Where(t => !t.IsDelete)
                .OrderByDescending(t => t.CreatDate)
                .AsQueryable();

            #region Filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Title.Contains(filter.Title));
            }
            #endregion

            #region Status

            switch (filter.FilterTicketStatus)
            {
                case FilterTicketStatus.All:
                    {
                        break;
                    }
                case FilterTicketStatus.Answered:
                    {
                        query = query.Where(c => c.Status == TicketStatusEnum.Answered);
                        break;
                    }
                case FilterTicketStatus.Pending:
                    {
                        query = query.Where(c => c.Status == TicketStatusEnum.Pending);
                        break;
                    }
                case FilterTicketStatus.Closed:
                    {
                        query = query.Where(c => c.Status == TicketStatusEnum.Closed);
                        break;
                    }

            }

            #endregion

            #region Part

            switch (filter.FilterTicketPart)
            {
                case FilterTicketPart.All:
                    {
                        break;
                    }
                case FilterTicketPart.Accounting:
                    {
                        query = query.Where(c => c.Part == TicketParts.Accounting);
                        break;
                    }
                case FilterTicketPart.Complaint:
                    {
                        query = query.Where(c => c.Part == TicketParts.Complaint);
                        break;
                    }
                case FilterTicketPart.Management:
                    {
                        query = query.Where(c => c.Part == TicketParts.Management);
                        break;
                    }
                case FilterTicketPart.Suggestion:
                    {
                        query = query.Where(c => c.Part == TicketParts.Suggestion);
                        break;
                    }
                case FilterTicketPart.Track:
                    {
                        query = query.Where(c => c.Part == TicketParts.Track);
                        break;
                    }
                case FilterTicketPart.Warranty:
                    {
                        query = query.Where(c => c.Part == TicketParts.Warranty);
                        break;
                    }
                case FilterTicketPart.etc:
                    {
                        query = query.Where(c => c.Part == TicketParts.etc);
                        break;
                    }
            }

            #endregion

            await filter.Paging(query);
            return filter;

        }

        public async Task<UserTicketsListViewModel> GetNoReadTickets(UserTicketsListViewModel filter)
        {
            var query = _context.Tickets
                .Include(t => t.TicketMassagesList)
                .Include(c => c.User)
                .Where(t => !t.IsDelete&&t.Status==TicketStatusEnum.Pending)
                .OrderByDescending(t => t.CreatDate)
                .AsQueryable();
            await filter.Paging(query);
            return filter;

        }

        public async Task<UserTicketsListViewModel> GetAllTicketsForUser(UserTicketsListViewModel filter)
        {
            var query = _context.Tickets
                .Include(t => t.TicketMassagesList)
                .Include(c => c.User)
                .Where(t => !t.IsDelete)
                .OrderByDescending(t => t.CreatDate)
                .AsQueryable();

            #region Filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Title.Contains(filter.Title));
            }
            #endregion

            #region Status

            switch (filter.Status)
            {
                case FilterTicketStatus.All:
                    {
                        break;
                    }
                case FilterTicketStatus.Answered:
                    {
                        query = query.Where(c => c.Status == TicketStatusEnum.Answered);
                        break;
                    }
                case FilterTicketStatus.Pending:
                    {
                        query = query.Where(c => c.Status == TicketStatusEnum.Pending);
                        break;
                    }
                case FilterTicketStatus.Closed:
                    {
                        query = query.Where(c => c.Status == TicketStatusEnum.Closed);
                        break;
                    }

            }

            #endregion

            #region Part

            switch (filter.Parts)
            {
                case FilterTicketPart.All:
                    {
                        break;
                    }
                case FilterTicketPart.Accounting:
                    {
                        query = query.Where(c => c.Part == TicketParts.Accounting);
                        break;
                    }
                case FilterTicketPart.Complaint:
                    {
                        query = query.Where(c => c.Part == TicketParts.Complaint);
                        break;
                    }
                case FilterTicketPart.Management:
                    {
                        query = query.Where(c => c.Part == TicketParts.Management);
                        break;
                    }
                case FilterTicketPart.Suggestion:
                    {
                        query = query.Where(c => c.Part == TicketParts.Suggestion);
                        break;
                    }
                case FilterTicketPart.Track:
                    {
                        query = query.Where(c => c.Part == TicketParts.Track);
                        break;
                    }
                case FilterTicketPart.Warranty:
                    {
                        query = query.Where(c => c.Part == TicketParts.Warranty);
                        break;
                    }
                case FilterTicketPart.etc:
                    {
                        query = query.Where(c => c.Part == TicketParts.etc);
                        break;
                    }
            }

            #endregion

            #region Levels

            switch (filter.Levels)
            {
                case FilterTicketLevels.All:
                    {
                        break;
                    }
                case FilterTicketLevels.Normal:
                    {
                        query = query.Where(c => c.Levels == TicketLevels.Normal);
                        break;
                    }
                case FilterTicketLevels.Important:
                    {
                        query = query.Where(c => c.Levels == TicketLevels.Important);
                        break;
                    }
                case FilterTicketLevels.VeryImportant:
                    {
                        query = query.Where(c => c.Levels == TicketLevels.VeryImportant);
                        break;
                    }

            }

            #endregion
            await filter.Paging(query);
            return filter;

        }

        public async Task<Ticket> AddTicketFromAdmin(Ticket ticket)
        {
          _context.Tickets.Add(ticket);
          await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<List<GetUserForAddTicketFromAdminViewModel>> GetUserForTicket()
        {
            return await _context.Users.Select(c =>
                new GetUserForAddTicketFromAdminViewModel()
                {
                    Id = c.Id,
                    Email = c.Email,
                }).ToListAsync();
        }

        public async Task<List<TicketMassages>> GetTicketMassagesForAdmin(int ticketId)
        {
          return await _context.TicketMassages.Where(c=>c.TicketId==ticketId).ToListAsync();

            
        }

        public async Task<TicketMassages> AddTicketMassageFromAdmin(TicketMassages massages)
        {
            _context.TicketMassages.Add(massages);
           await _context.SaveChangesAsync();
            return massages;
        }

        public Ticket geTicketById(int ticketId)
        {
           return _context.Tickets.Find(ticketId);
        }


        public async Task<Ticket> GetTicketForById(int ticketId)
        {
            return await _context.Tickets.Include(t=>t.TicketMassagesList).SingleOrDefaultAsync(t => t.Id == ticketId);

        }

        public async Task<List<TicketMassages>> GetMessagesByTicketId(int ticketId)
        {
            return await _context.TicketMassages
                .Where(t => t.TicketId == ticketId)
                .ToListAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
      
    }
}
