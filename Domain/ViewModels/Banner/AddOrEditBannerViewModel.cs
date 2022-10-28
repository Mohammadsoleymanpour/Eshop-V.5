using Domain.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ViewModels.Banner
{
    public class AddOrEditBannerViewModel
    {
        public int? Id { get; set; }
        public IFormFile? Image { get; set; }
        public string Link { get; set; }
        public BannerPosition Position { get; set; }
        public BannerCol Size { get; set; }
        public string? ImageName { get; set; }
    }


    public enum EditBannerPosition
    {
        [Display(Name = "بالاترین")] TopMost,
        [Display(Name = "بالا")] Top,
        [Display(Name = "اسلایدر")] Slide,
        [Display(Name = "راست بالا")] RightUP,
        [Display(Name = "راست پایین")] RightDown,
        [Display(Name = "پایین راست")] BottomRight,
        [Display(Name = "پایین چپ")] BottomLeft
    }
    public enum EditBannerSize
    {
        [Display(Name = "کل صفحه")] Full,
        [Display(Name = "سه چهارم صفحه")] OneEighth,
        [Display(Name = "نصف صفحه")] Half,
        [Display(Name = "یک چهارم")] Quarter
    }
}
