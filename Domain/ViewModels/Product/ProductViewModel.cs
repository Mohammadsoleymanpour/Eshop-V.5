using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Models.Product;
using Domain.ViewModels.Shared;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        [Display(Name = "نام کالا")]
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

        public string? Category { get; set; }
        public List<string> SubCategories { get; set; }
        public List<string> ProductImages { get; set; }

        public string ImageDefault { get; set; }

        public List<Feature> Features { get; set; }

        public int Id { get; set; }
        public int Price { get; set; }

        
      
    }

    public class FilterProductViewModel:BasePaging<Models.Product.Product>
    {
        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? Title { get; set; }
        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        public int Price { get; set; }
     

    }

    public class CreatProductViewModel
    {
        [Display(Name = "نام کالا")]
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
        public int? CategoryId { get; set; }

        public List<int>? SubGroupId { get; set; }


        public List<int>? FeatureValues { get; set; }
        public int? Prices { get; set; }
        
    }

    public class EditProductViewModel : ProductFeaturePartialViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام کالا")]
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
        public int? CategoryId { get; set; }
        public int PlusPrice { get; set; }
        public int PlusPriceId { get; set; }
        public List<int>? FeatureValues { get; set; }
        public List<int>? SubGroupId { get; set; }

    }

    public class AddImageGalleryViewModel
    {
       
        public bool IsDefault { get; set; }

        public int ProductId { get; set; }

        public IFormFile Image { get; set; }
    }


    public class EditImageGalleryViewModel
    {
        public int Id { get; set; }
        public string imageName { get; set; }
        public bool IsDefault { get; set; }

        public int ProductId { get; set; }

        public IFormFile Image { get; set; }
    }

    public class AddCommentViewModel
    {
        public int SenderId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
        public int? ParentId { get; set; }
        public int ProductId { get; set; }
        public CommentStatus.Status Status { get; set; }
    }

    public class CommentViewModel:BasePaging<ProductComment>
    {
        public int SenderId { get; set; }
        public string Comment { get; set; }
        public int? ParentId { get; set; }
        public int ProductId { get; set; }
        public CommentStatus.Status Status { get; set; }
    }

    public class FavoriteProductViewModel:BasePaging<FavoriteProduct>
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string? ProductTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public int Price { get; set; }
    }


    public class FilterProductByCategory:BasePaging<ProductSelectedCategory>
    {
        public int Id { get; set; }
        
        public string? SubCategoryName { get; set; }
        public string? Title { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public string OrderBy { get; set; }
    }

    public class FilterProduct : BasePaging<Models.Product.Product>
    {
        public string? Title { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public string OrderBy { get; set; }
    }
    public class FeatureViewModel:BasePaging<Feature>
    {
        public string? Title { get; set; }
        public FeatureEnums Type { get; set; }
    }

    public class AddFeatureViewModel 
    {
        public string Title { get; set; }
        public FeatureEnums Type { get; set; }
        public string Value { get; set; }
    }

   

    public class EditFeatureViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FeatureEnums Type { get; set; }
        public string? Value { get; set; }
    }

    
    public class FeaturesSelectListForProductViewModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }

    public class FeatureValuesSelectListForProductViewModel
    {
        public string Value { get; set; }
        public int Id { get; set; }
    }

    public class AddFeatureValueViewModel
    {
        public string Title { get; set; }

        public int FeatureId { get; set; }
    }

    public class EditFeatureValueViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int FeatureId { get; set; }
    }

    public class ProductFeaturePartialViewModel
    {
        public List<FeatureValue>? featureValues { get; set; }
        public List<ProductSelectedFeature>? ProductFeature { get; set; }
        public List<ProductPrice>? Prices { get; set; }

    }
}
