using Application.Interface;
using Domain.Interfaces;
using Domain.Models.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.FAQ;
using Domain.ViewModels.FAQ;

namespace Application.Services
{
    public class FaqService : IFaqService
    {
        #region Injections

        private IFaqRepository _faqRepository;

        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public Task<int> AddFaqFromAdmin(AddOrEditFaqViewModel faq)
        {
            var entity = new FAQ() 
            {
                Answer = faq.Answer,
                CreatDate = DateTime.Now,
                IsDelete =false,
                Question = faq.Qusetion
            };

            return _faqRepository.AddQuestion(entity);
        }

        public Task<bool> DeleteFaqFromAdmin(int faqId)
        {
            return _faqRepository.DeleteQuestion(faqId);
        }

        public async Task<bool> EditFaqFromAdmin(AddOrEditFaqViewModel faq)
        {
            var dbFaq =await _faqRepository.GetFaqById((int)faq.Id);
            dbFaq.Answer = faq.Answer;
            dbFaq.Question = faq.Qusetion;
            return await _faqRepository.UpdateFaq(dbFaq);
        }

        public async Task<FilterFaqViewModel> GetAllFaqsForAdmin(FilterFaqViewModel filter)
        {
            return await _faqRepository.GetAllFaqForAdmin(filter);
        }

        public async Task<AddOrEditFaqViewModel> GetFaqById(int faqId)
        {
            var faq = await _faqRepository.GetFaqById(faqId);
            var res = new AddOrEditFaqViewModel()
            {
                Answer = faq.Answer,
                Id = faq.Id,
                Qusetion = faq.Question
            };
            return res;
        }

        #endregion

        public async Task<List<FAQ>> GetFaqListAsync()
        {
            return await _faqRepository.GetAllFAQsAsync();
        }
    }
}
