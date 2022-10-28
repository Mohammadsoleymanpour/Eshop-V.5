using Domain.Models.Common;
using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ViewModels.Shared;

namespace Domain.ViewModels.DynamicLinks
{
    public class LinksForSiteViewModel
    {
        public List<DynamicLink> Links { get; set; }
        public PositionLinks Position { get; set; }
    }


    public class LinksForAdminViewModel : BasePaging<DynamicLink>
    {
        [Display(Name = "عنوان")]
       
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? Title { get; set; }
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkUrl { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public PositionLinksVM Position { get; set; }

        public DateTime? ExpirationDate { get; set; }

    }

    public class AddLinkViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? Title { get; set; }
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkUrl { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public PositionLinks Position { get; set; }

        public string? ExpirationDate { get; set; }

    }

    public class EditLinkViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? Title { get; set; }
        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LinkUrl { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public PositionLinks Position { get; set; }

        public string? ExpirationDate { get; set; }

    }

    public enum PositionLinksVM
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فوتر")]
        Footer,
        [Display(Name = "هدر")]
        Header,
    }
}
