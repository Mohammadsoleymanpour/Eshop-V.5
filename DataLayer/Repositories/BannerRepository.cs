using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Banner;
using Domain.ViewModels.Banner;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Banner;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Enums;

namespace DataLayer.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        #region Injections
        private ApplicationDbContext _context;

        public BannerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion


        public async Task<List<Banner>> GetBannerListAsync()
        {
            return await _context.Banners.ToListAsync();


        
        }

        public async Task<int> AddBanner(Banner banner)
        {
            await _context.AddAsync(banner);
             await Save();
            return banner.Id;
        }

        public async Task<FilterBannerViewModel> GetAllBanners(FilterBannerViewModel filter)
        {
            var query = _context.Banners.OrderByDescending(b => b.CreatDate).AsQueryable();

            #region Position
            switch (filter.Position)
            {
                case FilterPosistionBanner.All:
                    break;
                case FilterPosistionBanner.TopMost:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.TopMost);
                    break;
                case FilterPosistionBanner.Top:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.Top);
                    break;
                case FilterPosistionBanner.Slide:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.Slide);
                    break;
                case FilterPosistionBanner.RightUP:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.RightUP);
                    break;
                case FilterPosistionBanner.RightDown:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.RightDown);
                    break;
                case FilterPosistionBanner.BottomRight:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.BottomRight);
                    break;
                case FilterPosistionBanner.BottomLeft:
                    query = query.Where(b => b.Position == Domain.Models.Enums.BannerPosition.BottomLeft);
                    break;

            }
            #endregion

            await filter.Paging(query);
            return filter;

        }

        public async Task<Banner> GetBannerById(int id)
        {
            return await _context.Banners.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> RemoveBanner(int bannerId)
        {
            var bannerDb = await GetBannerById(bannerId);
            bannerDb.IsDelete = true;
            try
            {
                await UpdateBanner(bannerDb);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> Save()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<Banner> UpdateBanner(Banner banner)
        {
            _context.Update(banner);
            await Save();
            return banner;


        }

        public async Task<bool> IsBannerExist(BannerPosition position)
        {
            return await _context.Banners.AnyAsync(b => b.Position == position);
        }
    }
}
