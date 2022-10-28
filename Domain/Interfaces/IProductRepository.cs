using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Product;
using Domain.ViewModels.Product_Tags;
using Domain.ViewModels.Product;
using Domain.ViewModels.Product_Comment;
using Domain.ViewModels.ProductCategory;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        #region Product Category

        Task<int> AddCategory(ProductCategory category);
        Task<int> AddSubCategory(ProductCategory category);
        Task<List<ProductCategory>> GetAllCategories();
        Task<List<ProductCategory>> GetAllSubCategories(int id);
        Task<List<ProductCategory>> GetAllSubCategoriesForEditProduct(int id);
        Task<FilterCategoryProduct> GetAllSubCategoriesForAdmin(FilterCategoryProduct filter, int id);
        Task<bool> DeleteCategory(int categoryId);
        public Task<FilterCategoryProduct> GetAllCategoriesForAdmin(FilterCategoryProduct filter);
        Task<bool> UpdateCategory(ProductCategory category);
        Task<ProductCategory> GetCategoryById(int id, int? parentId);

        Task<bool> UpdateProductFromAdmin(Product product);
        Task<bool> UpdateSelectedCatProductFromAdmin(ProductSelectedCategory selectedCategory);

        #endregion

        #region Product Site

        Task<List<ProductCartViewModel>> GetAllProductForIndex();
        Task<Product> GetProductById(int productId);
        Task<List<Feature>> GetFeatureByPriceId(int priceId);
        Task<List<Product>> GetSimilarProduct(int productId);
        Task<List<string>> GetProductTitleForSearch(string term);
        #endregion

        #region Product Admin

        Task<FilterProductViewModel> GetProductForAdmin(FilterProductViewModel filter, int startPrice = 0, int endPrice = 0);
        Task<int> AddProductFromAdmin(Product Product);
        bool AddSelectedCategoryProductFromAdmin(ProductSelectedCategory selectedCategory);

        Task<Product> GetProductFromAdmin(int id);
        Task<ProductSelectedCategory> GetSelectedCategoryProductFromAdmin(int id);
        Task<bool> DeleteProductFromAdmin(Product product);
        void DeleteSelectedCategoryProductFromAdmin(int ProductId);
        List<int> getAllSubCategory(int productId);
        #endregion

        #region Product Tags Admin

        Task<FilterListTagViewModel> GetAllTagsOfProductForAdmin(FilterListTagViewModel filter, int productId);
        Task<ProductTag> GetTagById(int id);
        Task<int> AddTagToProduct(ProductTag tag);
        Task<bool> DeleteTag(ProductTag tag);
        Task<bool> UpdateTag(ProductTag tag);

        #endregion

        #region Tags Site

        public Task<List<ProductTag>> GetTagsForUser(int productId);

        #endregion

        #region ProductGallery

        Task<int> AddImageGalleryFromAdmin(ProductGallery model);
        Task<List<ProductGallery>> GetAllImageGalleryFromAdmin(int id);
        Task<ProductGallery> GetGalleryById(int id);

        Task<bool> DeleteImage(ProductGallery model);

        #endregion

        #region Product Comment

        Task<bool> AnswerComment(AnswerCommentViewModel answer);
        Task<FilterProductCommentsViewModel> GetAllCommentsByProductForAdmin(FilterProductCommentsViewModel filter);
        Task<ProductComment> GetCommentById(int commentId);
        Task<bool> DeleteCommentById(int commentId);
        Task<bool> AddComment(ProductComment comment);
        Task<List<ProductComment>> GetAllCommentsByProductId(int id);
        Task<int> AddCommentFromUser(ProductComment model);

        Task<string> GetDefaultImageById(int id);
        Task<CommentViewModel> GetCommentsByUserId(CommentViewModel filter, int userId);
        Task<bool> DeleteCommentFromUser(ProductComment model);

        #endregion

        #region FavoriteProductSite

        Task<FavoriteProduct> AddFavoriteProduct(FavoriteProduct model);
        Task<bool> DeleteFavoriteProduct(int id);
        Task<FavoriteProductViewModel> GetFavoriteProductForAdmin(FavoriteProductViewModel model, int userId);

        #endregion

        #region FilterProductByCategory

        Task<FilterProductByCategory> GetProductByCategorty(FilterProductByCategory filter, int categoryId);
        Task<FilterProductByCategory> GetProductByCategortyName(FilterProductByCategory filter, string productName);
        Task<FilterProductByCategory> GetAllProducts(FilterProductByCategory filter);

        #endregion

        #region Feature

        Task<FeatureViewModel> GetAllFeatureForAdmin(FeatureViewModel model);
        Task<List<Feature>> GetAllFetures();
        Task<int> AddFeatureFromAdmin(Feature model);
        Task<int> AddFeatureValueFromAdmin(FeatureValue model);
        Task<List<FeatureValue>> GetAllFeturesValuesById(int id);
        Task<List<FeatureValue>> GetAllFeatureValues();
        Task<int> AddProductPrice(int price, int productId);
        Task<Feature> GetFeatureById(int id);
        Task<bool> UpdateFeature(Feature feature);
        Task<bool> DeleteFeature(int id);
        Task<int> GetFeatureIdByFeatureValueId(int featureValueId);
        Task<bool> AddProductSelectedFeature(ProductSelectedFeature feature);
        Task<ProductSelectedFeature> GetProductSelectedFeaturesByPriceId(int priceId);
        Task<ProductPrice> GetProductPriceByProductId(int productId);
        Task<int> UpdateProductPrice(int proudctId, int price);
        Task<bool> DeleteProductSelectedFeature(int priceId);

        Task<int> AddFeatureValue(FeatureValue model);
        Task<List<FeatureValue>> GetAllValues(int featureId);

        Task<FeatureValue> GetFeatureValueByIdForEdit(int id);

        Task<bool> UpdateFeatureValue(FeatureValue model);

        Task<bool> DeleteFeatureValue(FeatureValue model);

        Task<List<ProductSelectedFeature>> GetAllFeaturesOfProduct(int productId);

        Task<List<ProductPrice>> GetAllPricesOfProduct(int productId);
        Task<List<ProductSelectedFeature>> GetAllFeaturesByProductPriceId(int priceId);

        Task<List<ProductSelectedFeature>> GetFeaturesByProductId(int productId);
        Task<bool> RemoveProductPrice(int priceId);
        Task<bool> RemoveFeatureFromProduct(int priceId);

        #endregion
        void Save();
    }
}
