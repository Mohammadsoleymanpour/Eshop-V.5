using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.ViewModels.DynamicPage;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DynamicPageRepository : IDynamicPageRepository
    {
        private ApplicationDbContext _context;

        public DynamicPageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddDynamicPage(DynamicPage model)
        {
            if (await _context.DynamicPages.AnyAsync(c => c.Title == model.Title))
            {
                return 0;
            }

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }


        public async Task<int> AddDynamicLink(DynamicLink link)
        {
            await _context.AddAsync(link);
            await _context.SaveChangesAsync();
            return link.Id;
        }


        public async Task<bool> DeleteDynamicPage(int id)
        {
            var page = await _context.DynamicPages.FindAsync(id);

            if (page == null)
            {
                return false;
            }


            page.IsDelete = true;
            _context.Update(page);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FilterDynamicPageViewModel> GetAllPagesForAdmin(FilterDynamicPageViewModel filter)
        {
            var query = _context.DynamicPages
                .OrderByDescending(p => p.CreatDate)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(p => p.Title.Contains(filter.Title));
            }


            #endregion

            await filter.Paging(query);
            return filter;
        }



        public async Task<DynamicPage> GetPageByTitle(string title)
        {
            return await _context.DynamicPages.Include(p=>p.DynamicLink).FirstOrDefaultAsync(p => p.Title == title);
        }

        public async Task<List<DynamicPage>> GetAllPagesForSite()
        {
            return await _context.DynamicPages.ToListAsync();
        }
    }

}
