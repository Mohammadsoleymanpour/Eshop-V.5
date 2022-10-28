using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
    public class SocialMedia:BaseEntity<int>
    {
        public SocialMediaPlatform PlatForm { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}
