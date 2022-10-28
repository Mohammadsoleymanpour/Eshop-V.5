using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public class ContactUsEnums
    {
        public enum ContactUsAnswer
        {
            [Display(Name = "عملیات با مشکل مواجه شد")] Faild,
            [Display(Name = "پیام مورد نظر یافت نشد")] NotFound,
            [Display(Name = "عملیات با موفقیت انجام شد")] Success,
        }
    }
}
