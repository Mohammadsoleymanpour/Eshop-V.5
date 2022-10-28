using Domain.Models.Enums;
using Domain.ViewModels.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Common;

namespace Application.Interface
{
    public interface ISocialMediaService
    {
        #region Admin

        Task<bool> EditSocialMediaLink(EditSocialMediaViewModel media);
        Task<FilterSocialMediaForAdminViewModel> GetAllMediasForAdmin(FilterSocialMediaForAdminViewModel filter);
        Task<bool> DeleteSocial(int id);

        Task<int> AddSocialMedia(AddSocialMediaLinkViewModel model);
        Task<EditSocialMediaViewModel> GetMediaById(int id);
        Task<bool> IsMediaAlreadyHasUrl(SocialMediaPlatform platform);

        #endregion


        #region Site

        Task<List<SocialMedia>> GeMediaForShow();
        #endregion
    }
}
