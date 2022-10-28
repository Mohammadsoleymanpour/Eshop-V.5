using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enums
{
    public enum FeatureEnums
    {
        [Display(Name = "ابعاد")]
        Dimensions,
        [Display(Name = "وزن")]
        Weight,
        [Display(Name = "ویژگی مخصوص")]
        SpecialFeatures,
        [Display(Name = "تراشه")]
        Chip,
        [Display(Name = "نوع پردازنده")]
        ProcessorType,
        [Display(Name = " رم ")]
        Ram,
        [Display(Name = " زمان معرفی")]
        IntroductionTime,
        [Display(Name = " جنس بدنه")]
        bodyStructure,
        [Display(Name = " Cpu")]
        Cpu,
        [Display(Name = " پردازنده مرکزی")]
        CentralpPocessor,
        [Display(Name = " پردازنده گرافیکی")]
        Gpu




    }
}
