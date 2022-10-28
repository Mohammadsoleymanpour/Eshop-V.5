using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        [Display(Name = "حذف")]
        public bool IsDelete { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        [Required]
        public DateTime CreatDate { get; set; }

        public BaseEntity()
        {
            IsDelete = false;
            CreatDate = DateTime.Now;
        }
    }
}
