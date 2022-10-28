using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.ViewModels.Product_Tags;
using Domain.ViewModels.Product;
using Domain.ViewModels.Product_Comment;
using Domain.ViewModels.ProductCategory;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;
using System.Runtime.CompilerServices;

namespace DataLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Injections

        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<int> AddCategory(ProductCategory category)
        {
            try
            {
                await _context.AddAsync(category);
                Save();
                return category.Id;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddSubCategory(ProductCategory category)
        {
            try
            {
                await _context.AddAsync(category);
                Save();
                return category.Id;
            }
            catch
            {
                return 0;
            }
        }



        public async Task<List<ProductCategory>> GetAllCategories()
        {
            return await _context.ProductCategories
                             .Where(c => c.ParnetId == null)
                             .ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllSubCategories(int id)
        {
            return await _context.ProductCategories
                              .Where(c => c.ParnetId != null && c.ParnetId == id)
                              .ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllSubCategoriesForEditProduct(int id)
        {
            return await _context.ProductCategories
                .Where(c => c.ParnetId != null && c.Id == id)
                .ToListAsync();
        }

        public async Task<FilterCategoryProduct> GetAllSubCategoriesForAdmin(FilterCategoryProduct filter, int id)
        {
            var res = _context.ProductCategories
                .Where(c => c.ParnetId == id).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                res = res.Where(c => c.Title.Contains(filter.CategoryName));

            }

            await filter.Paging(res);
            return filter;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            try
            {
                category.IsDelete = true;
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<FilterCategoryProduct> GetAllCategoriesForAdmin(FilterCategoryProduct filter)
        {
            var res = _context.ProductCategories
                .Where(c => c.ParnetId == null).AsQueryable();

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                res = res.Where(c => c.Title.Contains(filter.CategoryName));

            }

            await filter.Paging(res);
            return filter;

        }

        public async Task<bool> UpdateCategory(ProductCategory category)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<ProductCategory> GetCategoryById(int id, int? parentId)
        {
            var category = _context.ProductCategories;
            var result = new ProductCategory();
            if (parentId != null)
                result = await category.FirstOrDefaultAsync(c => c.Id == id && c.ParnetId == parentId);
            else
                result = await category.FirstOrDefaultAsync(c => c.Id == id);


            return result;
        }

        public async Task<bool> UpdateProductFromAdmin(Product product)
        {
            if (product != null)
            {

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateSelectedCatProductFromAdmin(ProductSelectedCategory selectedCategory)
        {
            if (selectedCategory != null)
            {
                _context.ProductSelectedCategories.Update(selectedCategory);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<ProductCartViewModel>> GetAllProductForIndex()
        {
            return await _context.Products.Include(c => c.ProductGalleries).OrderByDescending(p=>p.CreatDate).Take(9).Select(c => new ProductCartViewModel()
            {
                ImageName = c.ProductGalleries.FirstOrDefault(c => c.IsDefault).ImageName,
                Price = c.Price,
                ProductId = c.Id,
                ProductName = c.Title,

            }).ToListAsync();
        }


        public async Task<List<Feature>> GetFeatureByPriceId(int priceId)
        {
            return await _context.ProductSelectedFeatures.Include(c => c.Feature)
                .ThenInclude(c => c.FeatureValues)
                .Where(c => c.ProductPriceId == priceId).Select(f => f.Feature)
                .ToListAsync();


        }

        public async Task<List<Product>> GetSimilarProduct(int productId)

        {
            var product = await GetProductById(productId);
            var tt = _context.ProductSelectedCategories.FirstOrDefault(c=>c.ProductId==product.Id).CategoryId;
            var catId = _context.ProductSelectedCategories.Include(c=>c.Product).ThenInclude(c=>c.ProductGalleries).Where(c => c.CategoryId == tt).Select(c => c.Product).ToList();
           // var cat = _context.ProductSelectedCategories.Where(c => c.CategoryId == tt).ToList();
            //List<Product> res = new List<Product>(); 

            //foreach (var item in catId)
            //{
            //    if (item != null)
            //    {

            //        res.Add(item);
            //    }
            //}

            return catId;
        }

        public async Task<List<string>> GetProductTitleForSearch(string term)
        {
            return await _context.Products.Where(c => c.Title.Contains(term)).Select(c => c.Title).ToListAsync();
        }

        public async Task<FilterProductViewModel> GetProductForAdmin(FilterProductViewModel filter, int StartPrice = 0, int EndPrice = 0)
        {
            var query = _context.Products.AsQueryable();

            if (filter.Title != null)
            {
                query = query.Where(c => c.Title.Contains(filter.Title));
            }

            if (StartPrice != 0)
            {
                query = query.Where(c => c.Price > StartPrice);

            }
            if (EndPrice != 0)
            {
                query = query.Where(c => c.Price < EndPrice);

            }

            await filter.Paging(query);
            return filter;
        }

        public async Task<int> AddProductFromAdmin(Product Product)
        {
            _context.Products.Add(Product);

            await _context.SaveChangesAsync();
            return Product.Id;
        }

        public bool AddSelectedCategoryProductFromAdmin(ProductSelectedCategory selectedCategory)
        {
            _context.ProductSelectedCategories.Add(selectedCategory);
            _context.SaveChanges();
            return true;
        }

        public async Task<Product> GetProductFromAdmin(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<ProductSelectedCategory> GetSelectedCategoryProductFromAdmin(int id)
        {
            return await _context.ProductSelectedCategories.FirstOrDefaultAsync(c => c.ProductId == id);
        }

        public async Task<bool> DeleteProductFromAdmin(Product product)
        {
            if (product != null)
            {
                product.IsDelete = true;
                await UpdateProductFromAdmin(product);
                return true;
            }
            return false;
        }

        public void DeleteSelectedCategoryProductFromAdmin(int ProductId)
        {

            var res = _context.ProductSelectedCategories.Where(c => c.ProductId == ProductId).ToList();
            foreach (var productSelectedCategory in res)
            {
                _context.Remove(productSelectedCategory);
            }

        }

        public List<int> getAllSubCategory(int productId)
        {
            return _context.ProductSelectedCategories.Where(c => c.ProductId == productId).Select(c => c.CategoryId)
                .ToList();
        }

        public async Task<List<ProductTag>> GetTagsForUser(int productId)
        {
            return await _context.ProductTags.Where(c => c.ProductId == productId).ToListAsync();
        }

        public async Task<int> AddImageGalleryFromAdmin(ProductGallery model)
        {
            _context.ProductGalleries.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }
        public async Task<bool> AnswerComment(AnswerCommentViewModel answer)
        {
            var answerComment = new ProductComment()
            {
                Comment = answer.Answer,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = answer.ParentId,
                ProductId = answer.ProductId,
                SenderId = answer.SenderId,

            };

            var comment = await GetCommentById(answer.CommentId);

            try
            {
                await AddComment(answerComment);
                comment.Status = Domain.Models.Enums.CommentStatus.Status.Answered;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<FilterProductCommentsViewModel> GetAllCommentsByProductForAdmin(FilterProductCommentsViewModel filter)
        {
            var query = _context.ProductComments
                .Where(c => c.ProductId == filter.ProductId)
                .Include(c => c.Product)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatDate)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.SenderEmail))
            {
                query = query.Where(c => c.User.Email.Contains(filter.SenderEmail));
            }


            #endregion


            #region Status

            switch (filter.Status)
            {
                case CommentStatus.CommentFilterStatus.All:
                    break;
                case CommentStatus.CommentFilterStatus.Answered:
                    {
                        query = query.Where(c => c.Status == Domain.Models.Enums.CommentStatus.Status.Answered);
                        break;
                    }
                case CommentStatus.CommentFilterStatus.NotAnswered:
                    {
                        query = query.Where(c => c.Status == Domain.Models.Enums.CommentStatus.Status.NotAnswered);
                        break;
                    }
            }

            #endregion

            await filter.Paging(query);
            return filter;
        }

        public async Task<ProductComment> GetCommentById(int commentId)
        {
            return await _context.ProductComments
               .Include(c => c.User)
               .Include(p => p.Product)
               .Include(pc => pc.ProductComments)
               .FirstOrDefaultAsync(c => c.Id == commentId);

            //TODO cHECK
        }

        public async Task<bool> DeleteCommentById(int commentId)
        {
            var comment = await GetCommentById(commentId);
            comment.IsDelete = true;

            try
            {
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddComment(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<List<ProductComment>> GetAllCommentsByProductId(int id)
        {
            return await _context.ProductComments.Include(c => c.User).Where(c => c.ProductId == id).ToListAsync();
        }

        public async Task<int> AddCommentFromUser(ProductComment model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<string> GetDefaultImageById(int id)
        {
            var imageName = await _context.ProductGalleries.FirstOrDefaultAsync(c => c.IsDefault && c.ProductId == id);
            if (imageName == null)
            {
                return null;
            }
            return imageName.ImageName;
        }

        public async Task<CommentViewModel> GetCommentsByUserId(CommentViewModel filter, int userId)
        {
            var query = _context.ProductComments.Include(c => c.User).Include(c => c.Product).ThenInclude(c => c.ProductGalleries).Where(c => c.User.Id == userId);
            filter.TakeEntity = 5;
            await filter.Paging(query);
            return filter;
        }

        public async Task<bool> DeleteCommentFromUser(ProductComment model)
        {
            var comment = await GetCommentById(model.Id);
            comment.IsDelete = true;
            _context.ProductComments.Update(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FavoriteProduct> AddFavoriteProduct(FavoriteProduct model)
        {
            if (_context.FavoriteProducts.Any(c => c.ProductId == model.ProductId && c.UserId == model.UserId))
            {
                return model;
            }
            _context.FavoriteProducts.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteFavoriteProduct(int id)
        {
            var favorite = await _context.FavoriteProducts.FirstOrDefaultAsync(c => c.ProductId == id);
            if (favorite == null)
            {
                return false;
            }
            favorite.IsDelete = true;
            _context.FavoriteProducts.Update(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FavoriteProductViewModel> GetFavoriteProductForAdmin(FavoriteProductViewModel model, int userId)
        {
            var query = _context.FavoriteProducts.Include(c => c.Product).ThenInclude(c => c.ProductGalleries).Where(c => c.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(model.ProductTitle))
            {
                query = query.Where(c => c.Product.Title.Contains(model.ProductTitle));
            }

            await model.Paging(query);
            return model;
        }

        public async Task<FilterProductByCategory> GetProductByCategorty(FilterProductByCategory filter, int categoryId)
        {
            var query = _context.ProductSelectedCategories.Include(c => c.Product).ThenInclude(c => c.ProductGalleries)
                .Include(c => c.Product).ThenInclude(c => c.FavoriteProducts).Where(c => c.CategoryId == categoryId)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Product.Title.Contains(filter.Title));
            }

            if (filter.StartPrice != 0)
            {
                query = query.Where(c => c.Product.Price > filter.StartPrice);
            }

            if (filter.EndPrice != 0)
            {
                query = query.Where(c => c.Product.Price < filter.EndPrice);
            }

            switch (filter.OrderBy)
            {
                case "all":
                    break;
                case "expensive":
                    {
                        query = query.OrderByDescending(c => c.Product.Price);

                    }
                    break;
                case "cheep":
                    {
                        query = query.OrderBy(c => c.Product.Price);

                    }
                    break;
                case "newest":
                    {
                        query = query.OrderByDescending(c => c.Product.CreatDate);

                    }
                    break;
                case "popular":
                    {
                        query = query.OrderByDescending(c => c.Product.FavoriteProducts.Count);
                    }
                    break;
            }

            #endregion

            var product = _context.ProductSelectedCategories.Include(c => c.Product)
                .FirstOrDefault(c => c.CategoryId == categoryId).Product;



            var subcategories = _context.ProductSelectedCategories.Include(c => c.ProductCategory)
                .FirstOrDefault(c => c.CategoryId == categoryId && c.ProductCategory.ParnetId != null);
           
            filter.SubCategoryName = subcategories.ProductCategory.Title;
            filter.Id = product.Id;
            await filter.Paging(query);
            return filter;
        }

        public async Task<FilterProductByCategory> GetProductByCategortyName(FilterProductByCategory filter, string productName)
        {
            var query = _context.ProductSelectedCategories.Include(c => c.Product)
                .ThenInclude(c => c.ProductGalleries)
                .Include(c => c.Product)
                .ThenInclude(c => c.FavoriteProducts)
                .Where(c => c.Product.Title.Contains(productName))
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Product.Title.Contains(filter.Title));
            }

            if (filter.StartPrice != 0)
            {
                query = query.Where(c => c.Product.Price > filter.StartPrice);
            }

            if (filter.EndPrice != 0)
            {
                query = query.Where(c => c.Product.Price < filter.EndPrice);
            }

            switch (filter.OrderBy)
            {
                case "all":
                    break;
                case "expensive":
                    {
                        query = query.OrderByDescending(c => c.Product.Price);

                    }
                    break;
                case "cheep":
                    {
                        query = query.OrderBy(c => c.Product.Price);

                    }
                    break;
                case "newest":
                    {
                        query = query.OrderByDescending(c => c.Product.CreatDate);

                    }
                    break;
                case "popular":
                    {
                        query = query.OrderByDescending(c => c.Product.FavoriteProducts.Count);
                    }
                    break;
            }

            #endregion

            await filter.Paging(query);
            return filter;
        }

        public async Task<FilterProductByCategory> GetAllProducts(FilterProductByCategory filter)
        {
            var query = _context.ProductSelectedCategories.Include(c => c.Product)
                 .ThenInclude(c => c.ProductGalleries)
                 .Include(c => c.Product)
                 .ThenInclude(c => c.FavoriteProducts)
                 .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Product.Title.Contains(filter.Title));
            }

            if (filter.StartPrice != 0)
            {
                query = query.Where(c => c.Product.Price > filter.StartPrice);
            }

            if (filter.EndPrice != 0)
            {
                query = query.Where(c => c.Product.Price < filter.EndPrice);
            }

            switch (filter.OrderBy)
            {
                case "all":
                    break;
                case "expensive":
                    {
                        query = query.OrderByDescending(c => c.Product.Price);

                    }
                    break;
                case "cheep":
                    {
                        query = query.OrderBy(c => c.Product.Price);

                    }
                    break;
                case "newest":
                    {
                        query = query.OrderByDescending(c => c.Product.CreatDate);

                    }
                    break;
                case "popular":
                    {
                        query = query.OrderByDescending(c => c.Product.FavoriteProducts.Count);
                    }
                    break;
            }

            #endregion

            await filter.Paging(query);
            return filter;
        }

        public async Task<FeatureViewModel> GetAllFeatureForAdmin(FeatureViewModel filter)
        {
            var query = _context.Features
                .Include(v => v.FeatureValues)
                .AsQueryable();

            #region filter

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => c.Title.Contains(filter.Title));
            }


            #endregion

            await filter.Paging(query);
            return filter;

        }

        public async Task<int> AddFeatureFromAdmin(Feature model)
        {
            _context.Features.Add(model);

            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<int> AddFeatureValueFromAdmin(FeatureValue model)
        {
            _context.FeatureValues.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<Feature> GetFeatureById(int id)
        {
            var res = await _context.Features.Include(c => c.FeatureValues).FirstOrDefaultAsync(c => c.Id == id);
            if (res == null)
            {
                return null;
            }

            return res;

        }

        public async Task<bool> UpdateFeature(Feature feature)
        {
            var res = await GetFeatureById(feature.Id);

            res.productSelectedFeatures = feature.productSelectedFeatures;
            res.FeatureValues = feature.FeatureValues;
            res.Title = feature.Title;
            res.Type = feature.Type;

            _context.Features.Update(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeature(int id)
        {
            var model = await GetFeatureById(id);
            model.IsDelete = true;
            _context.Features.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductGallery>> GetAllImageGalleryFromAdmin(int id)
        {
            return await _context.ProductGalleries.Where(c => c.ProductId == id).ToListAsync();
        }

        public async Task<ProductGallery> GetGalleryById(int id)
        {
            return await _context.ProductGalleries.FindAsync(id);
        }

        public async Task<bool> DeleteImage(ProductGallery model)
        {
            var gallery = await GetGalleryById(model.Id);
            if (gallery == null)
            {
                return false;
            }
            gallery.IsDelete = true;
            _context.ProductGalleries.Update(gallery);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<int> AddFeatureValue(FeatureValue model)
        {
            _context.FeatureValues.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<List<FeatureValue>> GetAllValues(int featureId)
        {
            return await _context.FeatureValues.Where(c => c.FeatureId == featureId).ToListAsync();
        }

        public async Task<FeatureValue> GetFeatureValueByIdForEdit(int id)
        {
            return await _context.FeatureValues.FindAsync(id);
        }

        public async Task<bool> UpdateFeatureValue(FeatureValue model)
        {


            _context.FeatureValues.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeatureValue(FeatureValue model)
        {
            model.IsDelete = true;
            return await UpdateFeatureValue(model);

        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.Include(c => c.productPrices).ThenInclude(c => c.productSelectedFeatures).Include(p => p.ProductSelectedCategories)
                .ThenInclude(p => p.ProductCategory)
                .Include(p => p.ProductGalleries)
                .Include(p => p.productPrices)
                .ThenInclude(p => p.productSelectedFeatures).ThenInclude(c => c.Feature).ThenInclude(c => c.FeatureValues)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<FilterListTagViewModel> GetAllTagsOfProductForAdmin(FilterListTagViewModel filter, int productId)
        {
            var query = _context.ProductTags
                .Include(t => t.Product)
                .Where(t => t.ProductId == productId).AsQueryable();


            if (!string.IsNullOrEmpty(filter.TagName))
            {
                query = query.Where(t => t.TagName == filter.TagName);
            }

            await filter.Paging(query);
            return filter;

        }

        public async Task<int> AddTagToProduct(ProductTag tag)
        {
            _context.ProductTags.Add(tag);
            try
            {
                await _context.SaveChangesAsync();
                return tag.Id;
            }
            catch
            {
                return 0;
            }
        }



        public async Task<bool> DeleteTag(ProductTag tag)
        {
            var res = await GetTagById(tag.Id);
            res.IsDelete = true;
            try
            {
                Save();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<ProductTag> GetTagById(int id)
        {
            var tag = await _context.ProductTags.SingleOrDefaultAsync(t => t.Id == id);
            return tag;
        }

        public async Task<bool> UpdateTag(ProductTag tag)
        {
            try
            {
                _context.Update(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Feature>> GetAllFetures()
        {
            var res = await _context.Features.Include(v => v.FeatureValues)
                .Include(v => v.productSelectedFeatures).ThenInclude(c => c.productPrice).ToListAsync();
            return res;
        }

        public async Task<List<FeatureValue>> GetAllFeturesValuesById(int id)
        {
            var res = await _context.FeatureValues
                .Where(f => f.Id == id)
                .Include(v => v.Feature)
                .Include(v => v.productSelectedFeatures)
                .ToListAsync();
            return res;
        }

        public async Task<List<FeatureValue>> GetAllFeatureValues()
        {
            return await _context.FeatureValues.Include(f => f.Feature).ToListAsync();
        }

        public async Task<int> AddProductPrice(int price, int productId)
        {
            var productPrice = new ProductPrice()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                Price = price,
                ProductId = productId
            };
            _context.ProductPrices.Add(productPrice);
            await _context.SaveChangesAsync();
            return productPrice.Id;
        }

        public async Task<int> GetFeatureIdByFeatureValueId(int featureValueId)
        {
            var featureValue = await _context.FeatureValues.FirstOrDefaultAsync(fv => fv.Id == featureValueId);
            return featureValue.FeatureId;

        }

        public async Task<bool> AddProductSelectedFeature(ProductSelectedFeature feature)
        {
            _context.ProductSelectedFeatures.Add(feature);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductSelectedFeature> GetProductSelectedFeaturesByPriceId(int priceId)
        {
            var productPrice = await GetProductPriceByProductId(priceId);
            return await _context.ProductSelectedFeatures.FirstOrDefaultAsync(pf => pf.ProductPriceId == priceId);
        }

        public async Task<ProductPrice> GetProductPriceByProductId(int productId)
        {
            return await _context.ProductPrices.FirstOrDefaultAsync(pc => pc.ProductId == productId);
        }


        public async Task<int> UpdateProductPrice(int proudctId, int price)
        {
            var getPrice = await GetProductPriceByProductId(proudctId);
            getPrice.Price = price;
            _context.ProductPrices.Update(getPrice);
            await _context.SaveChangesAsync();
            return getPrice.Id;
        }

        public async Task<bool> DeleteProductSelectedFeature(int priceId)
        {

            var res = await _context.ProductSelectedFeatures.FirstOrDefaultAsync(f => f.ProductPriceId == priceId);
            res.IsDelete = true;
            return true;
        }

        public async Task<List<ProductSelectedFeature>> GetAllFeaturesOfProduct(int productId)
        {
            return await _context.ProductSelectedFeatures
                 .Include(psf => psf.productPrice)
                 .ThenInclude(p => p.Product)
                 .Include(p => p.featureValue)
                 .ThenInclude(fv => fv.Feature)
                 .ToListAsync();
        }

        public async Task<List<ProductPrice>> GetAllPricesOfProduct(int productId)
        {
            return await _context.ProductPrices
                .Where(p => p.ProductId == productId)
                .Include(p => p.productSelectedFeatures)
                .ThenInclude(p => p.featureValue)
                .ThenInclude(p => p.Feature)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<List<ProductSelectedFeature>> GetAllFeaturesByProductPriceId(int priceId)
        {
            return await _context.ProductSelectedFeatures
                .Where(p => p.ProductPriceId == priceId)
                .Include(p => p.featureValue)
                .ThenInclude(f => f.Feature)
                .ToListAsync();
        }

        public async Task<List<ProductSelectedFeature>> GetFeaturesByProductId(int productId)
        {
            var qurey = await _context.ProductPrices.Where(p => p.ProductId == productId)
               .ToListAsync();
            var res = qurey.Select(c => c.Id);
            var res2 = _context.ProductSelectedFeatures
                .Include(p => p.Feature)
                .Where(p => res.Any(a => a == p.ProductPriceId));

            return await res2.ToListAsync();


        }

        public async Task<bool> RemoveProductPrice(int priceId)
        {
            var price = await _context.ProductPrices.FirstOrDefaultAsync(p => p.Id == priceId);
            price.IsDelete = true;
            try
            {
                _context.Update(price);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveFeatureFromProduct(int priceId)
        {
            var productFeatures = await GetAllFeaturesByProductPriceId(priceId);
            foreach (var item in productFeatures)
            {
                item.IsDelete = false;
            }

            try
            {
                _context.Update(productFeatures);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
