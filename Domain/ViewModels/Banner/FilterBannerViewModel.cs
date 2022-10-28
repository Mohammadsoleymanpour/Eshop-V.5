using Domain.Models.Banner;
using Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Banner
{
    public class FilterBannerViewModel : BasePaging<Models.Banner.Banner>
    {
        public FilterPosistionBanner Position { get; set; }

    }


    public enum FilterPosistionBanner
    {
        [Display(Name = "همه")] All,
        [Display(Name = "بالاترین")] TopMost,
        [Display(Name = "بالا")] Top,
        [Display(Name = "اسلایدر")] Slide,
        [Display(Name = "راست بالا")] RightUP,
        [Display(Name = "راست پایین")] RightDown,
        [Display(Name = "پایین راست")] BottomRight,
        [Display(Name = "پایین چپ")] BottomLeft
    }


}
