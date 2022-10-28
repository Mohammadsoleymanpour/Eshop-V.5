using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.FAQ
{
    public class AddOrEditFaqViewModel
    {
        public int? Id { get; set; }
        public string Qusetion { get; set; }
        public string Answer { get; set; }
    }
}
