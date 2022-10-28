using Domain.Models.Common;
using Domain.ViewModels.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILoggerRepository
    {
        Task<FilterUserLogViewModel> GetAllLogsOfAdmins(FilterUserLogViewModel filterUserLogViewModel);
        Task<FilterUserLogViewModel> GetAllLoginLogs(FilterUserLogViewModel filterUserLogViewModel);

        Task<bool> AddLogToUser(Log log);
        Task<bool> Save();

    }
}
