using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Product;
using Domain.ViewModels.Product;
using Domain.ViewModels.Product_Comment;
using Domain.ViewModels.Product_Tags;
using Domain.ViewModels.ProductCategory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Interface
{
    public interface IProductService
    {
        #region Category Product
        /// <summary>
        /// Get All Category Product For ViewComponent In Index Layout
        /// </summary>
        /// <returns>
        /// </returns>
        Task<List<ProductCategory>> GetAllProductsCategoryAsync();

        /// <summary>
        /// Get All SubCategory Product For ViewComponent In Index Layout
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<ProductCategory>> GetAllSubProductCategoryAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<bool> DeleteCategory(int categoryId);

        /// <summary>
        /// This Method Get All Category For Admin For CRUD 
        /// </summary>
        /// <param name="filter">
        ///      
        ///   This param is a ViewModel 
        /// </param>
        /// <returns>
        ///     Have Paging And Filter
        /// </returns>
        Task<FilterCategoryProduct> GetCategoriesForAdmin(FilterCategoryProduct filter);

        /// <summary>
        /// This Method Get Sub Category From Category For Admin For CRUD   
        /// </summary>
        /// <param name="filter">
        ///     This Params Is A ViewModel And Can Filter SubCategory
        /// </param>
        /// <param name="id">
        ///     This Params IS a Category Id For Get SubCategory
        /// </param>
        /// <returns></returns>
        Task<FilterCategoryProduct> GetSubCategoriesForAdmin(FilterCategoryProduct filter, int id);

        /// <summary>
        ///     Admin By This Method Can Add Category
        /// </summary>
        /// <param name="model">
        ///     Is A ViewModel For Add Category
        /// </param>
        /// <returns>
        ///     If Returns IS True Category Added To Bank
        /// </returns>
        Task<int> AddCategory(EditOrAddCategoryProduct model);

        /// <summary>
        /// Admin By This Method Can Add SubCategory
        /// </summary>
        /// <param name="name">
        ///     This Params Is A Title Of SubCategory
        /// </param>
        /// <param name="parentId">
        ///     This Params Is A Category ID For Add SubCategory In Category  
        /// </param>
        /// <returns>
        ///     If Return True SubCategory Added
        /// </returns>
        Task<int> AddSubCategory(string name, int parentId);

        /// <summary>
        ///   Admin By This Method Can Update Category
        /// </summary>
        /// <param name="model">
        ///     This Params Is A ViewModel For Add Or Edit Category
        /// </param>
        /// <returns>
        ///      If Return True Category Updated
        /// </returns>
        Task<bool> UpdateCategory(EditOrAddCategoryProduct model);

        /// <summary>
        ///  This Method For Get Category Or SubCategory
        /// </summary>
        /// <param name="id">
        ///     This Param For ID Category Or SubCategory
        /// </param>
        /// <param name="parentId">
        ///    This Params For Set Return Category Or SubCategory
        ///    
        /// </param>
        /// <returns>
        /// If This Params Is Null Method Returned Category
        ///  If This Params Have Value Method Returned SubCategory
        /// </returns>
        Task<ProductCategory> GetCategoryById(int id, int? parentId);

        /// <summary>
        ///  This Method For Get Category Or SubCategory
        /// </summary>
        /// <param name="id">
        ///     This Param For ID Category Or SubCategory
        /// </param>
        /// <param name="parentId">
        ///    This Params For Set Return Category Or SubCategory
        ///    
        /// </param>
        /// <returns>
        ///
        ///     Returend Is A ViewModel And :
        /// If This Params Is Null Method Returned Category
        ///  If This Params Have Value Method Returned SubCategory
        /// </returns>
        Task<EditOrAddCategoryProduct> GetCategoryByIdForAdmin(int id, int? parentId);

        #endregion

        #region Product Site
        Task<List<Product>> GetSimilarProduct(int productId);
        /// <summary>
        ///     Get Product For Site Index
        /// </summary>
        /// <returns>
        ///     List From View Model 
        /// </returns>
        Task<List<ProductCartViewModel>> GetAllProductForIndex();

        /// <summary>
        ///     This Method Get Single Product For Show Detail 
        /// </summary>
        /// <param name="productId">
        ///     This Params Is A Product Id For Show
        /// </param>
        /// <returns>
        ///   
        /// </returns>
        Task<ProductViewModel> GetProductByIdForDetail(int productId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ProductCartViewModel> GetProductByIdForCart(int productId);

        Task<List<string>> GetProductTitleForSearch(string term);

        Task<FilterProductByCategory> GetAllProducts(FilterProductByCategory filter);

        #endregion

        #region Product Admin
        /// <summary>
        ///  Admin By This Method Can Filter Product And Paging Product
        /// </summary>
        /// <param name="filter">
        ///     This Param For Filter By Title And Paging Product
        /// </param>
        /// <param name="StartPrice">
        ///     This Params For Filter Product By min Price
        /// </param>
        /// <param name="EndPrice">
        ///     This Params For Filter Product By min Price
        /// </param>
        /// <returns> A View Model From Product</returns>
        Task<FilterProductViewModel> GetProductForAdmin(FilterProductViewModel filter, int StartPrice = 0, int EndPrice = 0);
        /// <summary>
        /// Get Category For Add Product In View Add Product
        /// </summary>
        /// <returns>
        ///     list From Select List Item
        /// Value Is A Category Id
        /// And Text Is A Category Title
        /// </returns>
        Task<List<SelectListItem>> GetCategoriesForAddProductAdmin();
        /// <summary>
        /// Get SubCategory For Add Product In View Add Product
        /// </summary>
        /// <param name="id">
        /// Is A Category Id
        /// For Get All Sub Category 
        /// </param>
        /// <returns></returns>
        Task<List<SelectListItem>> GetSubCategoriesForAddProductAdmin(int id);
        /// <summary>
        /// This Method For Add Product From Admin
        /// </summary>
        /// <param name="product">
        ///  This Is A ViewModel For Create Product
        /// </param>
        /// <returns>
        ///  Returned Added Product Id
        /// </returns>
        Task<int> AddProductFromAdmin(CreatProductViewModel product);
        /// <summary>
        /// This Method Get Product For Edit Or Delete Product
        /// </summary>
        /// <param name="id">
        ///     This Params Is A Product Id
        /// </param>
        /// <returns> View Model For Edit </returns>

        Task<EditProductViewModel> GetProductFromAdmin(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SelectListItem>> GetAllSubCategoriesForEditProduct(int id);
        /// <summary>
        /// This Method Update Product From Admin 
        /// </summary>
        /// <param name="product">
        ///     Is A ViewModel For Edit Or Delete
        /// </param>
        /// <returns> If Returned True Product Updated</returns>
        Task<bool> UpdateProductFromAdmin(EditProductViewModel product);
        /// <summary>
        /// This Method Delete Product From Admin 
        /// </summary>
        /// <param name="product">
        /// Is A ViewModel For Edit Or Delete
        /// </param>
        /// <returns>
        /// If Returned True Product Deleted
        /// </returns>
        Task<bool> DeleteProductFromAdmin(EditProductViewModel product);

        #endregion

        #region Tags Admin

        /// <summary>
        /// Get All Peoduct's Tags For Admin With Paging And Filter
        /// </summary>
        /// <param name="filter">A VewModel With Search Filter And Paging</param>
        /// <param name="productId">Id of Product</param>
        /// <returns>A VewModel With Search Filter And Paging</returns>
        Task<FilterListTagViewModel> GetAllProductTagsForAdmin(FilterListTagViewModel filter ,int productId);
       
        /// <summary>
        /// Get A Tag By Id
        /// </summary>
        /// <param name="id">Id Of Tag</param>
        /// <returns>A View Model For Tag</returns>
        Task<TagViewModel> GetTagById(int id);

        /// <summary>
        /// Delete A Tag From DataBase
        /// </summary>
        /// <param name="id">Id Of Tag To Delete</param>
        /// <returns></returns>
        Task<bool> DeleteTag(int id);

        /// <summary>
        /// Add A New Tag To Product
        /// </summary>
        /// <param name="tag">A ViewModel Of Needed Data Of Tag</param>
        /// <param name="productId">Id Of Product To Add A Tag</param>
        /// <returns>A Boolean That Indicate The Result Of Opration</returns>
        Task<int> AddTagToProduct(TagViewModel tag,int productId);

        /// <summary>
        /// Update A Tag Of Product From Admin
        /// </summary>
        /// <param name="tag">A ViewModel Of Needed Info Of Tag</param>
        /// <param name="productId">Id Of Product</param>
        /// <returns>A Boolean That Indicate The Result Of Opration</returns>
        Task<bool> UpdateTag(TagViewModel tag , int productId);

        #endregion

        #region Tags Site
        /// <summary>
        /// This Method Get Product Tags For Show In A Product View
        /// </summary>
        /// <param name="productId">
        ///     IS A Product Id 
        /// </param>
        /// <returns></returns>
        public Task<List<ProductTag>> GetTagsForUser(int productId);

        #endregion

        #region ProductImages
        /// <summary>
        /// This Method Add Image And Set Image Name
        /// </summary>
        /// <param name="model">
        ///     Is A ViewModel For Add Image
        /// </param>
        /// <returns>
        ///     Returned Added Image Gallery Id
        /// </returns>
        Task<int> AddImageFromAdmin(AddImageGalleryViewModel model);
        Task<int> AddImageFromAdmin(EditImageGalleryViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<ProductGallery>> GetAllImageGalleryFromAdmin(int id);
        /// <summary>
        ///  Get Image For Edit Or Add
        /// </summary>
        /// <param name="id">
        ///     Is A Image Gallery Id
        /// </param>
        /// <returns></returns>
        Task<ProductGallery> GetGalleryById(int id);
        /// <summary>
        /// This Method Delete Images
        /// </summary>
        /// <param name="model">
        ///     IS A model From TBL
        /// </param>
        /// <returns></returns>
        Task<bool> DeleteImage(ProductGallery model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">
        ///     Is A Product Id
        /// </param>
        /// <returns></returns>
        Task<string> GetDefaultImageById(int id);

        #endregion

        #region Product Comments Admin

        /// <summary>
        /// Get All Of Product's Comment With Filter Search Filter And Paging
        /// </summary>
        /// <param name="filter">A ViewModel Of Comments With Search Field And Paging</param>
        /// <returns>A ViewModel Of Comments With Search Field And Paging</returns>
        Task<FilterProductCommentsViewModel> GetAllCommentOfProductForAdmin(FilterProductCommentsViewModel filter);
        
        /// <summary>
        /// Get A Comment For Admin
        /// </summary>
        /// <param name="commentId">Id Of Comment</param>
        /// <returns>A ViewModel Of Comment With Needed Data</returns>
        Task<AnswerCommentViewModel> GetCommentForAdmin(int commentId);
        
        /// <summary>
        /// Delete A Comment From Admin
        /// </summary>
        /// <param name="commentId">Id Of Comment</param>
        /// <returns>A Boolean That Indicate The Result </returns>
        Task<bool> DeleteCommentByIdFromAdmin(int commentId);

        /// <summary>
        /// Answer A Comment From Admin
        /// </summary>
        /// <param name="answerComment">A ViewModel OF Comments To Answer A Comment</param>
        /// <returns>A Boolean That Indicate The Result </returns>
        Task<bool> AnswerCommentFromAdmin(AnswerCommentViewModel answerComment);

        #endregion

        #region ProductComment Site

        /// <summary>
        /// Get All Of Product's Comment
        /// </summary>
        /// <param name="id">Id Of Product</param>
        /// <returns>List Of Product's Comment Model</returns>
        Task<List<ProductComment>> GetAllCommentsByProductId(int id);

        /// <summary>
        /// Add A New Comment To Product By User
        /// </summary>
        /// <param name="model">A View Model Of Neede Data Of Tag</param>
        /// <returns>Id Of Newly Added Comment</returns>
        Task<int> AddCommentFromUser(AddCommentViewModel model);
        
        /// <summary>
        /// Get List Of Comments Of A User With Filter Search Filter And Paging
        /// </summary>
        /// <param name="filter">A ViewModel Of Comment With Search Field And Paging</param>
        /// <param name="userId">Id Of Current User </param>
        /// <returns>A ViewModel Of Comments Of User With Paging</returns>
        Task<CommentViewModel> GetCommentsByUserId(CommentViewModel filter, int userId);

        /// <summary>
        /// Delete A User By User
        /// </summary>
        /// <param name="id">Id Of Comment</param>
        /// <returns>A Boolean That Indicate The Result</returns>
        Task<bool> DeleteCommentFromUser(int id);


        #endregion

        #region FavoriteProduct
        /// <summary>
        /// This Method Add Favorite Product
        /// </summary>
        /// <param name="prId">
        /// Is Product Id For Add
        /// </param>
        /// <param name="UserId">
        /// IS a User Id For Add
        /// </param>
        /// <returns> Returned Model Added</returns>
        Task<FavoriteProduct> AddFavoriteProduct(int prId, int UserId);
        Task<bool> DeleteFavoriteProduct(int id);
        /// <summary>
        ///  This Method Get Favorite Product For Admin And User
        /// </summary>
        /// <param name="model">
        ///     View Model Favorite Product
        /// </param>
        /// <param name="userId">
        /// Is a UserId
        /// </param>
        /// <returns> ViewModel For Favorite Product</returns>

        Task<FavoriteProductViewModel> GetFavoriteProductForAdmin(FavoriteProductViewModel model, int userId);
        #endregion

        #region FilterProductByCategory
        /// <summary>
        /// Get Product By Category For Filter Product IN Site And Have Paging And Filter
        /// </summary>
        /// <param name="filter">
        ///     Is A ViewModel For Get Product By Category IN Filter Category
        /// </param>
        /// <param name="categoryId"></param>
        /// <returns>
        /// ViewModel
        /// </returns>
        Task<FilterProductByCategory> GetProductByCategorty(FilterProductByCategory filter, int categoryId);

        Task<FilterProductByCategory> GetProductByCategortyName(FilterProductByCategory filter, string productName);
        #endregion

        #region Feature
        /// <summary>
        /// This Method Get All Feature For Admin And Have Paging And Filter
        /// </summary>
        /// <param name="model">
        ///     This Is A ViewModel
        /// </param>
        /// <returns></returns>
        Task<FeatureViewModel> GetAllFeatureForAdmin(FeatureViewModel model);
        /// <summary>
        /// This Method Add Feature From Admin
        /// </summary>
        /// <param name="model">
        /// ViewModel For Add Feature 
        /// </param>
        /// <returns>
        ///     Feature Added Id
        /// </returns>
        Task<int> AddFeatureFromAdmin(AddFeatureViewModel model);
        Task<bool> EditFeatureFromAdmin(EditFeatureViewModel model);
        /// <summary>
        /// This Method Get Feature By Id For Edit Or Delete
        /// </summary>
        /// <param name="id">
        /// Feature Id
        /// </param>
        /// <returns>
        ///  Feature ViewModel 
        /// </returns>
        Task<EditFeatureViewModel> GetFeatureById(int id);

        /// <summary>
        /// This Method Deleted Feature
        /// </summary>
        /// <param name="id">
        ///     Feature Id
        /// </param>
        /// <returns>
        ///   If Feature Deleted Returned True
        /// </returns>
        Task<bool> DeleteFeature(int id);
        /// <summary>
        /// This Method Added Feature Value
        /// </summary>
        /// <param name="model">
        ///  Add Feature ViewModel
        /// </param>
        /// <returns>
        ///  Returned Feature Value Added ID
        /// </returns>
        Task<int> AddFeatureValue(AddFeatureValueViewModel model);
        /// <summary>
        /// This Method Get ALL Feature Values
        /// </summary>
        /// <param name="featureId">
        ///  Feature Id
        /// </param>
        /// <returns> List Feature Values</returns>
        Task<List<FeatureValue>> GetAllValues(int featureId);

        /// <summary>
        /// This Method Get Feature Value By Id
        /// </summary>
        /// <param name="id">
        ///  Feature Value Id
        /// </param>
        /// <returns>
        ///     ViewModel For Edit Or Deleted
        /// </returns>
        Task<EditFeatureValueViewModel> GetFeatureValueByIdForEdit(int id);
        /// <summary>
        /// This Method Update Value By View Model
        /// </summary>
        /// <param name="model">
        ///     This Is A ViewModel For Edit
        /// </param>
        /// <returns>
        ///     If Feature Value Updated Returned True
        /// </returns>
        Task<bool> UpdateFeatureValue(EditFeatureValueViewModel model);

        /// <summary>
        /// This Method Deleted Feature Values
        /// </summary>
        /// <param name="model">
        ///  Is A View Model For Edit OR Delete Feature Value
        /// </param>
        /// <returns>
        ///     If Feature Value Deleted Returned True
        /// </returns>
        Task<bool> DeleteFeatureValue(EditFeatureValueViewModel model);
        #endregion


        #region Fetures For Product
        /// <summary>
        /// This Method IN Add Product View Get Feature
        /// </summary>
        /// <returns> List From Feature</returns>
        Task<List<Feature>> GetFeaturesForAddProduct();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        Task<List<SelectListItem>> GetProductFeatureValuesByFeatureId(int featureId);
        /// <summary>
        ///  This Method Get All Feature Value 
        /// </summary>
        /// <returns>
        ///     List From FeatureValue
        /// </returns>
        Task<List<FeatureValue>> GetAllFeatureValues();

        /// <summary>
        /// Get All Product's Feature With ProductSelectedFeatures
        /// </summary>
        /// <param name="productId">Id Of product</param>
        /// <returns>List Of Model Product Selected Feature</returns>
        Task<List<ProductSelectedFeature>> GetAllProductFeatures(int productId);

        /// <summary>
        /// Add A New Feature To Product Using Product Price And Product Selected Features
        /// </summary>
        /// <param name="featureValues">List Of Inteager Of All New Feature Values</param>
        /// <param name="plusPrice">Price Of The New Features</param>
        /// <param name="productId">Id Of The Product</param>
        /// <returns>A Boolean That Indicate The Result Of Opration</returns>
        Task<bool> AddFeatureToProduct(List<int> featureValues,int plusPrice,int productId);


        Task<List<ProductPrice>> GetAllPricesOfProduct(int productId);


        Task<List<ProductSelectedFeature>> GetAllFeaturesByProductPriceId(int priceId);

        Task<List<ProductSelectedFeature>> GetAllFeaturesSelected(int productId);

        Task<bool> DeleteFeatureFromProduct( int priceId);

        #endregion
    }
}
