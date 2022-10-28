using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Common;
using Domain.Models.Enums;
using Domain.ViewModels.DynamicLinks;

namespace Domain.Interfaces
{
    public interface IDynamicRepository
    {
        #region Admin

        Task<int> AddLink(DynamicLink model);

        Task<DynamicLink> GetLinkById(int id);

        Task<bool> Updatelink(DynamicLink model);

        Task<bool> Deletelink(int id);

        Task<LinksForAdminViewModel> GetLinksForAdmin(LinksForAdminViewModel filter);

        #endregion

        #region Site

        Task<List<DynamicLink>> GetLinksByPosition(PositionLinks position);

            #endregion
        Task Save();

    }
}
