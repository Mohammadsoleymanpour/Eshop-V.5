using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.DynamicPage
{


    public class DynamicPageViewModelAdmin
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UrlLink { get; set; }

    }
}
