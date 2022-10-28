using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public enum PositionLinks
    {
        [Display(Name = "فوتر")]
        Footer,
        [Display(Name = "هدر")]
        Header,
    }
}
