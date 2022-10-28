using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.FAQ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.FAQ;
using Microsoft.EntityFrameworkCore;
using Domain.ViewModels.FAQ;

namespace DataLayer.Repositories
{
    public class FaqRepository:IFaqRepository
    {
        #region Injections

        private ApplicationDbContext _context;

        public FaqRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion


        public async Task<int> AddQuestion(FAQ faq)
        {
            await _context.AddAsync(faq);
             await Save();
            return faq.Id;
        }

        public async Task<bool> DeleteQuestion(int faqId)
        {
            var faq =await GetFaqById(faqId);
            faq.IsDelete = true;
            return await UpdateFaq(faq);
        }

        public async Task<FilterFaqViewModel> GetAllFaqForAdmin(FilterFaqViewModel filter)
        {
            var query = _context.FAQs
                .OrderByDescending(f => f.CreatDate)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Question))
            {
                query = query.Where(f => f.Question.Contains(filter.Question));
            }
            if (!string.IsNullOrEmpty(filter.Answer))
            {
                query = query.Where(f => f.Answer.Contains(filter.Answer));
            }

            #endregion

            await filter.Paging(query);
            return filter;
        }

        public async Task<FAQ> GetFaqById(int faqId)
        {
            return await _context.FAQs.FirstOrDefaultAsync(f => f.Id == faqId);
        }

        public async Task<bool> Save()
        {
            try
            {
               await _context.SaveChangesAsync();
            }
            catch
            {

                return false;
            }
            return true;
        }

        public async Task<bool> UpdateFaq(FAQ faq)
        {
            _context.Update(faq);
            return await Save();
        }


        public async Task<List<FAQ>> GetAllFAQsAsync()
        {
            return await _context.FAQs.ToListAsync();
        }

    }
}
