using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Common;
using Domain.ViewModels.DynamicPage;

namespace Application.Interface
{
    public interface IDynamicPageService
    {

  
        Task<FilterDynamicPageViewModel> GetAllPagesForAdmin(FilterDynamicPageViewModel filter);

        Task<int> AddDynamicPage(DynamicPageViewModelAdmin model);
      
        Task<DynamicPage> GetPageByTitle(string title);
        Task<bool> DeleteDynamicPage(int id);

        Task<List<DynamicPage>> GetAllPagesForSite();

    }
}
