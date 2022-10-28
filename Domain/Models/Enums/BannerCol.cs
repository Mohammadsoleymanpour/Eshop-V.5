using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models.Enums
{
    public enum BannerCol
    {
        [Display(Name = "کل صفحه")] Full,
        [Display(Name = "سه چهارم صفحه")] OneEighth,
        [Display(Name = "نصف صفحه")] Half,
        [Display(Name = "یک چهارم")] Quarter
    }
}
