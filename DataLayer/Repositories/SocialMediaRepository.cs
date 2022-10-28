using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        #region Injections

        private ApplicationDbContext _context;

        public SocialMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        #endregion


        public async Task<bool> Delete(int id)
        {
            var media = await _context.SocialMedias.FirstOrDefaultAsync(s=>s.Id==id);
            if (media == null)
                return false;
            try
            {
                media.IsDelete = true;
                media.Url = "";
                await Update(media);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public  IQueryable<SocialMedia> GetAllSocialMediaForAdmin()
        {
            return  _context.SocialMedias.AsQueryable();
        }

        public async Task<SocialMedia> GetMediaById(int id)
        {
            return await _context.SocialMedias.FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<List<SocialMedia>> GeMediaForShow()
        {
            var res= await _context.SocialMedias.ToListAsync();
            return res;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
           
        }

        public async Task<bool> Update(SocialMedia socialMedia)
        {
            try
            {
                _context.Update(socialMedia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<int> AddSocialMedia(SocialMedia media)
        {
            try
            {
                await _context.AddAsync(media);
                await Save();
                return media.Id;
            }
            catch 
            {

                return 0;
            }
        }

        public async Task<bool> IsMediaAlreadyHasUrl(SocialMediaPlatform platform)
        {
            return await _context.SocialMedias.AnyAsync(s => s.PlatForm == platform);
        }
    }
}
