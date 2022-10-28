using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.Models.Enums;
using Domain.ViewModels.DynamicLinks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DynamicLinkRepository:IDynamicRepository
    {
        private ApplicationDbContext _context;

        public DynamicLinkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddLink(DynamicLink model)
        {
            _context.DynamicLinks.Add(model);
           await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<DynamicLink> GetLinkById(int id)
        {
            return await _context.DynamicLinks.FindAsync(id);

        }

        public async Task<bool> Updatelink(DynamicLink model)
        {
            _context.DynamicLinks.Update(model);
           await _context.SaveChangesAsync();
           return true;
        }

        public async Task<bool> Deletelink(int id)
        {
            var link = await GetLinkById(id);
            if (link != null)
            {
                return false;
            }
            link.IsDelete=true;
          return await Updatelink(link);
        }

        public async Task<LinksForAdminViewModel> GetLinksForAdmin(LinksForAdminViewModel filter)
        {
            var query = _context.DynamicLinks.AsQueryable();

            #region filter

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Title.Contains(filter.Title));
            }

            #endregion

            await filter.Paging(query);
            return filter;
        }

        public async Task<List<DynamicLink>> GetLinksByPosition(PositionLinks position)
        {
            return await _context.DynamicLinks.Where(x => x.Position == position && x.ExpirationDate>=DateTime.Now).ToListAsync();
        }

        public Task Save()
        {
           return _context.SaveChangesAsync();
        }
    }
}
