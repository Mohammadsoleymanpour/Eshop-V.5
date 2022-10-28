using Domain.ViewModels.DynamicLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Common;

namespace Application.Interface
{
    public interface IDynamicLinkService
    {
        Task<int> AddLink(AddLinkViewModel model);

        Task<EditLinkViewModel> GetViewModelLinkById(int id);
        Task<DynamicLink> GetLinkById(int id);

        Task<bool> Updatelink(EditLinkViewModel model);

        Task<bool> Deletelink(int id);
        Task<LinksForAdminViewModel> GetLinksForAdmin(LinksForAdminViewModel filter);

        #region Site

        Task<LinksForSiteViewModel> GetLinksForHeader();
        Task<LinksForSiteViewModel> GetLinksForFooter();

        #endregion
    }
}
