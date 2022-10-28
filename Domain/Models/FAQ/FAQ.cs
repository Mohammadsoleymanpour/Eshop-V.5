using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.FAQ
{

    public class FAQ:BaseEntity<int>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

}
