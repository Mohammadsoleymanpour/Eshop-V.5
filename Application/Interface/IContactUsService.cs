using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Domain.Models.ContactUss;
using Domain.ViewModels.ContactUs;

namespace Application.Interface
{
    public interface IContactUssService
    {
        /// <summary>
        /// This Method Get All Of Messages Send From Contact Us in Form Of Filter Paging
        /// Also Has Paging And The Ability To Filter Query
        /// </summary>
        /// <param name="filter">
        /// A FIlterContactUsViewModel That Has Base Paging And Fields For Filter Search
        /// </param>
        /// <returns>List Of ContactUs That Has Paging And Has Been Filtered</returns>
        Task<FilterContactUssViewModel> GetContactUsss(FilterContactUssViewModel filter);

        /// <summary>
        /// This Method Add A Contact Us Ticket From User To Database
        /// </summary>
        /// <param name="ContactUss">A ViewModel That Has All Requiared Fields of Contact Us Model</param>
        /// <returns>The Newly Saved Contact Us</returns>
        ContactUss AddContactUssFromUser(ContactUssViewModel ContactUss);

        /// <summary>
        /// A Method To Answer A Contact Us Tichek From Admin
        /// </summary>
        /// <param name="answerId">int Contact Id For Getting Contact Us To Answer </param>
        /// <param name="answerMessage"> The Answer That Admin Has Given To Ticket</param>
        /// <returns>An Enum That Indicate Result Of Opration </returns>
        Enum AnswerContactUsTicket(int answerId, string answerMessage);

        /// <summary>
        /// A Qurey That Get A Contact Us By Id From DataBase In Form Of ViewModel
        /// </summary>
        /// <param name="id">Contact Us Id For Query</param>
        /// <returns>A View Model For Answering Contact Us From Admin </returns>
        AnswerContactUsViewModel GetContactById(int id);

    }
}
