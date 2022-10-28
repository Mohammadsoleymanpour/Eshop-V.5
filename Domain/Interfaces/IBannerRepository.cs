using Domain.Models.Banner;
using Domain.ViewModels.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Banner;
using Domain.Models.Enums;

namespace Domain.Interfaces
{
    public interface IBannerRepository
    {



        #region Site

        Task<List<Banner>> GetBannerListAsync();

        #endregion

        #region Admin
        
        Task<int> AddBanner(Banner banner);
        Task<bool> RemoveBanner(int bannerId);
        Task<Banner> GetBannerById(int id);
        Task<FilterBannerViewModel> GetAllBanners(FilterBannerViewModel filter);
        Task<Banner> UpdateBanner(Banner banner);
        Task<bool> IsBannerExist(BannerPosition position);

        #endregion
        Task<bool> Save();

    }
}
