using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Application.Interface;
using Application.Security;
using Application.Sender;
using Domain.Interfaces;
using Domain.Models.Enums;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Domain.ViewModels.ContactUs;
using Domain.Models.ContactUss;
using Application.Convertor;

namespace Application.Services
{
    public class ContactUssService : IContactUssService
    {
        IContactUssRepository _ContactUssRepository;

        public ContactUssService(IContactUssRepository ContactUssRepository)
        {
            _ContactUssRepository = ContactUssRepository;
        }
        public ContactUss AddContactUssFromUser(ContactUssViewModel ContactUss)
        {
            ContactUss addContactUss = new ContactUss()
            {
                Subject = ContactUss.Subject,
                Body = ContactUss.Body,
                CreatDate = DateTime.Now,
                Email = FixedText.FixEmail(ContactUss.Email),
                FullName = ContactUss.FullName,
                IsDelete = false,
                PhoneNumber = ContactUss.PhoneNumber,
                Status = ContactUssEnums.Status.NotAnswered, 
                IP = ContactUss.Ip
            };
           
            return _ContactUssRepository.AddContactUss(addContactUss);
        }

       

        public  Enum AnswerContactUsTicket(int answerId, string answerMessage)
        {
            try
            {
                _ContactUssRepository.AnswerContactUsTicket(answerId,answerMessage);
                
            }
            catch
            {
                return ContactUsEnums.ContactUsAnswer.NotFound;
            }

            return ContactUsEnums.ContactUsAnswer.Success;

        }

        public AnswerContactUsViewModel GetContactById(int id)
        {
          var ticket = _ContactUssRepository.GetContactUsById(id);
            var res = new AnswerContactUsViewModel()
            {
                Email = ticket.Email,
                PhoneNumber = ticket.PhoneNumber,
                AnswerBody = "",
                Body = ticket.Body,
                FullName = ticket.FullName,
                Subject = ticket.Subject
        };

            return res;

        }

        public  async Task<FilterContactUssViewModel> GetContactUsss(FilterContactUssViewModel filter)
        {
            var query = await _ContactUssRepository.GetAllContactUssForAdmin(filter);

        
            return filter;
        }
    }
}
