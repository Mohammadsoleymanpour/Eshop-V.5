using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public class CommentStatus
    {
        public enum Status
        {
           [Display(Name = "پاسخ داده شده")] Answered,
           [Display(Name = "بدون پاسخ")]NotAnswered
        }
    }
}
