using Domain.Models.Common;
using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.DynamicPage
{
    public class FilterDynamicPageViewModel:BasePaging<Models.Common.DynamicPage>
    {
        public string Title { get; set; }
    }
}
