using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ViewModels.SocialMedia
{
    public class AddSocialMediaLinkViewModel
    {
        public string Url { get; set; }
        public AddPlatForm PlatForm { get; set; }
    }

    public enum AddPlatForm
    {
        [Display(Name = "تویتتر")] Twitter,
        [Display(Name = "تلگرام")] Telegram,
        [Display(Name = "فیس بوک")] FaceBook,
        [Display(Name = "اینستاگرام")] Instagram
    }
}
