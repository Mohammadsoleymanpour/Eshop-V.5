using Domain.Models.Common;
using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SocialMedia
{
    public class FilterSocialMediaForAdminViewModel : BasePaging<Models.Common.SocialMedia>
    {
        public FilterPlatform Platform { get; set; }
        

    }



    public enum FilterPlatform
    {
        [Display(Name = "همه")] All,
        [Display(Name = "تویتتر")] Twitter,
        [Display(Name = "تلگرام")] Telegram,
        [Display(Name = "فیس بوک")] FaceBook,
        [Display(Name = "اینستاگرام")] Instagram
    }
}
