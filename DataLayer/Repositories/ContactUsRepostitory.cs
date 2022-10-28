using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.ContactUss;
using Domain.Models.Enums;
using Domain.ViewModels.ContactUs;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Repositories
{
    public class ContactUssRepository : IContactUssRepository
    {
        private ApplicationDbContext _context;

        public ContactUssRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ContactUss> GetAllContactUss()
        {
            return _context.ContactUss.ToList();
        }

        public ContactUssViewModel GetContactUsById(int id)
        {
            var contact = _context.ContactUss.FirstOrDefault(c => c.Id == id);

            var res = new ContactUssViewModel()
            {
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Body = contact.Body,
                FullName = contact.FullName,
                Subject = contact.Subject,
                Ip = contact.IP
            };

            return res;
        }

        public ContactUss GetContactUsByIdForAnswer(int id)
        {
            return _context.ContactUss.FirstOrDefault(c => c.Id == id);
        }


        public async Task<FilterContactUssViewModel> GetAllContactUssForAdmin(FilterContactUssViewModel filter)
        {
            var query = _context.ContactUss
                .Where(c => c.IsDelete != true)
                .OrderByDescending(c => c.CreatDate)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(t => t.Email.Contains(filter.Email));
            }
            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(t => t.PhoneNumber.Contains(filter.PhoneNumber));
            }
            #endregion

            #region Status

            switch (filter.Status)
            {
                case FilterContactUssStatus.All:
                    break;
                case FilterContactUssStatus.NotAnswered:
                    query = query.Where(t => t.Status == ContactUssEnums.Status.NotAnswered);
                    break;
                case FilterContactUssStatus.Answered:
                    query = query.Where(t => t.Status == ContactUssEnums.Status.Answered);
                    break;
            }

            #endregion

            #region Subject

            switch (filter.Subject)
            {
                case FilterContactUssSubject.All:
                    break;
                case FilterContactUssSubject.Warranty:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.Warranty);
                    break;
                case FilterContactUssSubject.Accounting:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.Accounting);
                    break;
                case FilterContactUssSubject.Suggestion:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.Suggestion);
                    break;
                case FilterContactUssSubject.Etc:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.etc);
                    break;
                case FilterContactUssSubject.Managment:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.Management);
                    break;
                case FilterContactUssSubject.Complaint:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.Complaint);
                    break;
                case FilterContactUssSubject.Track:
                    query = query.Where(t => t.Subject == ContactUssEnums.Subject.Track);
                    break;
            }

            #endregion


            await filter.Paging(query);
            return filter;
        }

        public ContactUss AddContactUss(ContactUss ContactUss)
        {
            _context.ContactUss.Add(ContactUss);
            _context.SaveChanges();
            return ContactUss;
        }

        public void UpdateContactUss(ContactUss ContactUss)
        {
            _context.ContactUss.Update(ContactUss);
            _context.SaveChanges();

        }

        public void DeleteContactUss(ContactUss ContactUss)
        {
            ContactUss.IsDelete = true;
            UpdateContactUss(ContactUss);
        }

        public bool AnswerContactUsTicket(int contactId, string answerMessage)
        {
            var ticket = GetContactUsByIdForAnswer(contactId);
            ticket.Status = ContactUssEnums.Status.Answered;
            ticket.Answer = answerMessage;
            try
            {

                UpdateContactUss(ticket);

            }
            catch
            {
                return false;
            }



            return true;
        }
    }
}
