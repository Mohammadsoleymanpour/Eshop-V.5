using Domain.Models.Common;
using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISocialMediaRepository
    {
        #region Admin

        Task<bool> Update(SocialMedia socialMedia);
        Task<bool> Delete(int id);
        Task<SocialMedia> GetMediaById(int id);

        public IQueryable<SocialMedia> GetAllSocialMediaForAdmin();

        Task<int> AddSocialMedia(SocialMedia media);

        Task<bool> IsMediaAlreadyHasUrl(SocialMediaPlatform platform);


        #endregion

        #region Site

        Task<List<SocialMedia>> GeMediaForShow();
        #endregion
        Task Save();
    }
}
