using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Votes;

namespace Domain.Models.Product
{
    public class Product : BaseEntity<int>
    {
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        #region Relations


        public List<FavoriteProduct> FavoriteProducts { get; set; }
        public List<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        public List<ProductGallery> ProductGalleries { get; set; }

        public List<ProductPrice> productPrices { get; set; }

        public List<ProductTag> ProductTags { get; set; }

        public List<ProductVotes> ProductVotesList { get; set; }

        public List<ProductComment> ProductComments { get; set; }
        #endregion

    }
}
