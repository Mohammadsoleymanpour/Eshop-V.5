using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SocialMedia
{
    public class EditSocialMediaViewModel
    {
        public string Url { get; set; }
        public SocialMediaPlatform Platform { get; set; }
        public int Id { get; set; }
    }
}
