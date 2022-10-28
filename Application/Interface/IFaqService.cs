using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.FAQ;
using Domain.ViewModels.FAQ;

namespace Application.Interface
{
    public interface IFaqService
    {

        #region Admin
        Task<FilterFaqViewModel> GetAllFaqsForAdmin(FilterFaqViewModel filter);
        Task<int> AddFaqFromAdmin(AddOrEditFaqViewModel faq);
        Task<bool> EditFaqFromAdmin(AddOrEditFaqViewModel faq);
        Task<bool> DeleteFaqFromAdmin(int faqId);
        Task<AddOrEditFaqViewModel> GetFaqById(int faqId);
#endregion
        #region Site

        Task<List<FAQ>> GetFaqListAsync();


        #endregion
    }
}
