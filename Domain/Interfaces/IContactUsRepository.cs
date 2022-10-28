using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ContactUss;
using Domain.ViewModels.ContactUs;

namespace Domain.Interfaces
{
    public interface IContactUssRepository
    {
        List<ContactUss> GetAllContactUss();
        ContactUssViewModel GetContactUsById(int id);
        ContactUss GetContactUsByIdForAnswer(int id);
        Task<FilterContactUssViewModel> GetAllContactUssForAdmin(FilterContactUssViewModel filter);
        ContactUss AddContactUss(ContactUss ContactUss);
        void UpdateContactUss(ContactUss ContactUss);
        void DeleteContactUss(ContactUss ContactUss);
        bool AnswerContactUsTicket(int contactId,string answerMessage);
    }
}
