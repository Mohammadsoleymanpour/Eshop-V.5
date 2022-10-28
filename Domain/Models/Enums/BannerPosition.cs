using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models.Enums
{
    public enum BannerPosition
    {

        [Display(Name = "بالاترین")] TopMost,
        [Display(Name = "بالا")] Top,
        [Display(Name = "اسلایدر")] Slide,
        [Display(Name = "راست بالا")] RightUP,
        [Display(Name = "راست پایین")] RightDown,
        [Display(Name = "پایین راست")] BottomRight,
        [Display(Name = "پایین چپ")] BottomLeft
    }
}
