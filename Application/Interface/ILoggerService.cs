using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;
using Domain.ViewModels.Log;

namespace Application.Interface
{
    public interface ILoggerService
    {
        Task<bool> AddLog(int entityId,int userId,string message,LogType? logType);
        Task<bool> AddLog(int entityId, int userId, string message);
        Task<FilterUserLogViewModel> GetLog(FilterUserLogViewModel filter);
        Task<FilterUserLogViewModel> GetLastTenLogs(FilterUserLogViewModel filter);
        Task<FilterUserLogViewModel> GetUserLoginLogs(FilterUserLogViewModel filter);
    }
}
