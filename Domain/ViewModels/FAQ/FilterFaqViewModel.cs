using Domain.Models.FAQ;
using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.FAQ
{
    public class FilterFaqViewModel : BasePaging<Models.FAQ.FAQ>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
