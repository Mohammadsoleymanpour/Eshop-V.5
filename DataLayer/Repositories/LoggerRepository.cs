using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.Models.Enums;
using Domain.ViewModels.Log;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private ApplicationDbContext _context;

        public LoggerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddLogToUser(Log log)
        {
            await _context.AddAsync(log);
            return await Save();
        }

        public async Task<FilterUserLogViewModel> GetAllLoginLogs(FilterUserLogViewModel filterUserLogViewModel)
        {
            var query = _context.Logs.Include(l => l.User).OrderByDescending(l => l.CreatDate).AsQueryable();

            query = query.Where(l => l.LogType == LogType.UserLogin);

            #region User

            if (!string.IsNullOrEmpty(filterUserLogViewModel.UserName))
            {
                query = query.Where(l => l.User.Email.Contains(filterUserLogViewModel.UserName));
            }


            #endregion

            #region Activity

            if (!string.IsNullOrEmpty(filterUserLogViewModel.Activity))
            {
                query = query.Where(l => l.Desctiption.Contains(filterUserLogViewModel.Activity));
            }

            #endregion

            await filterUserLogViewModel.Paging(query);
            return filterUserLogViewModel;
        }

        public async Task<FilterUserLogViewModel> GetAllLogsOfAdmins(FilterUserLogViewModel filterUserLogViewModel)
        {
            var query = _context.Logs.Include(l => l.User).OrderByDescending(l => l.CreatDate).AsQueryable();

            query = query.Where(l => l.LogType == LogType.AdminActivity);

            #region User

            if (!string.IsNullOrEmpty(filterUserLogViewModel.UserName))
            {
                query = query.Where(l => l.User.Email.Contains(filterUserLogViewModel.UserName));
            }


            #endregion

            #region Activity

            if (!string.IsNullOrEmpty(filterUserLogViewModel.Activity))
            {
                query = query.Where(l => l.Desctiption.Contains(filterUserLogViewModel.Activity));
            }

            #endregion

            await filterUserLogViewModel.Paging(query);
            return filterUserLogViewModel;
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
    }
}
