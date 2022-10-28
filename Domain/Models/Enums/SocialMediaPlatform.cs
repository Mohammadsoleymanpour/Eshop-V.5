using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public enum SocialMediaPlatform
    {
        [Display(Name = "توییتر")] Twitter,
        [Display(Name = "تلگرام")] Telegram,
        [Display(Name = "فیس بوک")] FaceBook,
        [Display(Name = "اینستاگرام")] Instagram
    }
}
