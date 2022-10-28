using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;

namespace Domain.Models.Banner
{
    public class Banner:BaseEntity<int>
    {
        public string ImageName { get; set; }

        public BannerPosition Position { get; set; }

        public BannerCol BannerCol { get; set; }

        public string? Link { get; set; }

    }
}
