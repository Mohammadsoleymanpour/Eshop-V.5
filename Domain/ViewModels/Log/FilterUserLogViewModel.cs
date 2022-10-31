using Domain.Models.Common;
using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Log
{
    public class FilterUserLogViewModel:BasePaging<Models.Common.Log>
    {
        public string? UserName { get; set; }
        public string? Activity { get; set; }
        
    }
}
