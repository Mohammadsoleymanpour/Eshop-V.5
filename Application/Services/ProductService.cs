using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Convertor;
using Application.Interface;
using Application.Security;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Product;
using Domain.ViewModels.Product;
using Domain.ViewModels.Product_Comment;
using Domain.ViewModels.Product_Tags;
using Domain.ViewModels.ProductCategory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductCategory>> GetAllProductsCategoryAsync()
        {
            return await _productRepository.GetAllCategories();
        }

        public async Task<List<ProductCategory>> GetAllSubProductCategoryAsync(int id)
        {
            return await _productRepository.GetAllSubCategories(id);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            return await _productRepository.DeleteCategory(categoryId);
        }
        public async Task<FilterCategoryProduct> GetCategoriesForAdmin(FilterCategoryProduct filter)
        {

            var query = await _productRepository.GetAllCategoriesForAdmin(filter);

            return query;
        }

        public async Task<FilterCategoryProduct> GetSubCategoriesForAdmin(FilterCategoryProduct filter, int id)
        {
            var query = await _productRepository.GetAllSubCategoriesForAdmin(filter, id);

            return query;
        }

        public async Task<int> AddCategory(EditOrAddCategoryProduct category)
        {
            var cat = new ProductCategory()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParnetId = category.ParentId,
                Title = category.CategoryName
            };
            return await _productRepository.AddCategory(cat);

        }

        public async Task<int> AddSubCategory(string name, int parentId)
        {

            var cat = new ProductCategory()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParnetId = parentId,
                Title = name
            };
            return await _productRepository.AddCategory(cat);



        }

        public async Task<bool> UpdateCategory(EditOrAddCategoryProduct model)
        {
            var cat = await GetCategoryById(Convert.ToInt32(model.CategoryId), model.ParentId);

            cat.Title = model.CategoryName;
            if (model.IsDeleted != null && model.IsDeleted == true)
            {
                cat.IsDelete = true;
            }


            try
            {
                await _productRepository.UpdateCategory(cat);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductCategory> GetCategoryById(int id, int? parentId)
        {
            var res = await _productRepository.GetCategoryById(id, parentId);
            return res;
        }

        public async Task<ProductCartViewModel> GetProductByIdForCart(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            var res = new ProductCartViewModel()
            {
                ProductId = product.Id,
                ImageName = product.ProductGalleries.FirstOrDefault(i => i.IsDefault).ImageName,
                Price = product.Price,
                ProductName = product.Title
            };

            return res;
        }

        public async Task<List<string>> GetProductTitleForSearch(string term)
        {
            return await _productRepository.GetProductTitleForSearch(term);
        }

        public Task<FilterProductByCategory> GetAllProducts(FilterProductByCategory filter)
        {
            return _productRepository.GetAllProducts(filter);
        }

        public Task<FilterProductViewModel> GetProductForAdmin(FilterProductViewModel filter, int StartPrice = 0, int EndPrice = 0)
        {
            return _productRepository.GetProductForAdmin(filter, StartPrice, EndPrice);
        }

        public async Task<List<SelectListItem>> GetCategoriesForAddProductAdmin()
        {
            var cat = await _productRepository.GetAllCategories();

            return cat.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetSubCategoriesForAddProductAdmin(int id)
        {
            var cat = await _productRepository.GetAllSubCategories(id);

            return cat.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task<int> AddProductFromAdmin(CreatProductViewModel product)
        {
            var addProduct = new Product()
            {
                CreatDate = DateTime.Now,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                IsDelete = false,
                Price = product.Price,
                Title = product.Title,

            };

            int id = await _productRepository.AddProductFromAdmin(addProduct);
            foreach (var item in product.SubGroupId)
            {
                var selectedcat = new ProductSelectedCategory()
                {
                    CreatDate = DateTime.Now,
                    CategoryId = item,
                    IsDelete = false,
                    ProductId = id,

                };
                bool status = _productRepository.AddSelectedCategoryProductFromAdmin(selectedcat);
                if (status != true)
                {
                    return 0;
                }
            }
            if (product.Prices == null)
                product.Prices = 0;
            int changePrice = await _productRepository.AddProductPrice((int)product.Prices, id);

            if (product.FeatureValues!=null)
            {
                foreach (var item in product.FeatureValues)
                {
                    var selectedFeature = new ProductSelectedFeature()
                    {
                        CreatDate = DateTime.Now,
                        FeatureId = await _productRepository.GetFeatureIdByFeatureValueId(item),
                        FeatureValueId = item,
                        ProductPriceId = changePrice

                    };
                    bool status = await _productRepository.AddProductSelectedFeature(selectedFeature);
                    if (status != true)
                    {
                        return 0;
                    }

                }
            }
            var addImage = new ProductGallery();
            string imagepath = "";
            addImage.ImageName = "NoProductImage.png";
            var img = "NoProductImage.png";
            imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img", addImage.ImageName);

          

            ImageConvertor ImageResize = new ImageConvertor();
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb", addImage.ImageName);

            ImageResize.Image_resize(imagepath, thumbPath, 200);

            addImage.IsDefault = true;
            addImage.ProductId = addProduct.Id;
            addImage.IsDelete = false;
            addImage.CreatDate = DateTime.Now;
            await _productRepository.AddImageGalleryFromAdmin(addImage);
            return id;

        }

        public async Task<EditProductViewModel> GetProductFromAdmin(int id)
        {
            var product = await _productRepository.GetProductById(id);
            var selectedCategory = await _productRepository.GetSelectedCategoryProductFromAdmin(id);
            List<int> subSelected = _productRepository.getAllSubCategory(product.Id);
            var viewModel = new EditProductViewModel()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                CategoryId = selectedCategory.CategoryId,
                Price = product.Price,
                SubGroupId = subSelected,
                FeatureValues = product.productPrices.First(p => p.ProductId == id).productSelectedFeatures.Select(f => f.FeatureValueId).ToList(),
                PlusPrice = product.productPrices.FirstOrDefault(p => p.ProductId == id).Price,
                PlusPriceId = product.productPrices.FirstOrDefault(p => p.ProductId == id).Id,
                Prices =await _productRepository.GetAllPricesOfProduct(id),
                
            };


            return viewModel;
        }

        public async Task<List<SelectListItem>> GetAllSubCategoriesForEditProduct(int id)
        {
            var selected = await _productRepository.GetAllSubCategoriesForEditProduct(id);

            return selected.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Title,
            }).ToList();

        }

        public async Task<bool> UpdateProductFromAdmin(EditProductViewModel product)
        {
            var GetProduct = await _productRepository.GetProductById(product.Id);
            var selectedCat = await _productRepository.GetSelectedCategoryProductFromAdmin(product.Id);
            var selectedFeature = await _productRepository.GetProductSelectedFeaturesByPriceId(product.PlusPriceId);
            if (GetProduct != null || selectedCat != null || selectedFeature != null)
            {

                GetProduct.Title = product.Title;
                GetProduct.Description = product.Description;
                GetProduct.ShortDescription = product.ShortDescription;
                GetProduct.Price = product.Price;

                foreach (var i in product.SubGroupId)
                {
                    _productRepository.DeleteSelectedCategoryProductFromAdmin(GetProduct.Id);


                    var selectedcat = new ProductSelectedCategory()
                    {
                        CreatDate = DateTime.Now,
                        CategoryId = i,
                        IsDelete = false,
                        ProductId = GetProduct.Id,

                    };
                    bool status = _productRepository.AddSelectedCategoryProductFromAdmin(selectedcat);
                }
                int priceId = await _productRepository.UpdateProductPrice(product.Id, (int)product.PlusPrice);
                foreach (var item in product.FeatureValues)
                {
                    await _productRepository.DeleteProductSelectedFeature(priceId);
                    var feature = new ProductSelectedFeature()
                    {
                        CreatDate = DateTime.Now,
                        FeatureId = await _productRepository.GetFeatureIdByFeatureValueId(item),
                        FeatureValueId = item,
                        ProductPriceId = product.PlusPriceId

                    };
                    bool status = await _productRepository.AddProductSelectedFeature(selectedFeature);


                }
                return await _productRepository.UpdateProductFromAdmin(GetProduct);

            }

            return false;
        }

        public async Task<bool> DeleteProductFromAdmin(EditProductViewModel product)
        {
            var getProduct = await _productRepository.GetProductById(product.Id);
            return await _productRepository.DeleteProductFromAdmin(getProduct);
        }

        public Task<List<Product>> GetSimilarProduct(int productId)
        {
            return _productRepository.GetSimilarProduct(productId);
        }

        public Task<List<ProductCartViewModel>> GetAllProductForIndex()
        {
            return _productRepository.GetAllProductForIndex();
        }

        public async Task<ProductViewModel> GetProductByIdForDetail(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            var feRes = product.productPrices.FirstOrDefault(c => c.ProductId == product.Id);
            var queryRes = await _productRepository.GetFeatureByPriceId(feRes.Id);

            var res = new ProductViewModel()
            {
                Title = product.Title,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                //Category = product.ProductSelectedCategories.FirstOrDefault(c => c.ProductCategory.ParnetId == null).ProductCategory.Title,
                SubCategories = product.ProductSelectedCategories.Where(c => c.ProductCategory.ParnetId != null).Select(c => c.ProductCategory.Title).ToList(),
                ProductImages = product.ProductGalleries.Where(pg => !pg.IsDefault).Select(g => g.ImageName).ToList(),
                ImageDefault = product.ProductGalleries.First(g => g.IsDefault == true).ImageName,
                Id = product.Id,
                Features = queryRes
            };

            return res;
        }

        public async Task<FilterListTagViewModel> GetAllProductTagsForAdmin(FilterListTagViewModel filter, int productId)
        {
            return await _productRepository.GetAllTagsOfProductForAdmin(filter, productId);
        }

        public async Task<TagViewModel> GetTagById(int id)
        {
            var tag = await _productRepository.GetTagById(id);
            var res = new TagViewModel()
            {
                TagId = tag.Id,
                TagName = tag.TagName
            };
            return res;
        }

        public async Task<bool> DeleteTag(int id)
        {
            var tag = await _productRepository.GetTagById(id);
            tag.IsDelete = true;
            try
            {
                await _productRepository.UpdateTag(tag);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> AddTagToProduct(TagViewModel tag, int productId)
        {
            var add = new ProductTag()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                ProductId = productId,
                TagName = tag.TagName
            };

            try
            {
               return  await _productRepository.AddTagToProduct(add);
                
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> UpdateTag(TagViewModel tag, int productId)
        {
            var update = new ProductTag()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                ProductId = productId,
                TagName = tag.TagName
            };

            try
            {
                await _productRepository.UpdateTag(update);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<List<ProductTag>> GetTagsForUser(int productId)
        {
            return _productRepository.GetTagsForUser(productId);
        }

        public async Task<int> AddImageFromAdmin(AddImageGalleryViewModel model)
        {
            var addImage = new ProductGallery();
            if (model.Image != null )
            {
                string imagepath = "";
                addImage.ImageName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(model.Image.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img", addImage.ImageName);

                await using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

                ImageConvertor ImageResize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb", addImage.ImageName);

                ImageResize.Image_resize(imagepath, thumbPath, 200);

                addImage.IsDefault = model.IsDefault;
                addImage.ProductId = model.ProductId;
                addImage.IsDelete = false;
                addImage.CreatDate = DateTime.Now;
                return await _productRepository.AddImageGalleryFromAdmin(addImage);
            }
            else
            {
                string imagepath = "";
                addImage.ImageName = "NoProductImage.png";
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img", addImage.ImageName);

                await using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

                ImageConvertor ImageResize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb", addImage.ImageName);

                ImageResize.Image_resize(imagepath, thumbPath, 200);

                addImage.IsDefault = model.IsDefault;
                addImage.ProductId = model.ProductId;
                addImage.IsDelete = false;
                addImage.CreatDate = DateTime.Now;
                return await _productRepository.AddImageGalleryFromAdmin(addImage);
            }



            return 0;
        }

        public async Task<int> AddImageFromAdmin(EditImageGalleryViewModel model)
        {
            var addImage = new ProductGallery();
            if (model.Image != null )
            {
                string imagepath = "";
                addImage.ImageName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(model.Image.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img", addImage.ImageName);

                await using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

                ImageConvertor ImageResize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb", addImage.ImageName);

                ImageResize.Image_resize(imagepath, thumbPath, 200);

                addImage.IsDefault = model.IsDefault;
                addImage.ProductId = model.ProductId;
                addImage.IsDelete = false;
                addImage.CreatDate = DateTime.Now;
                return await _productRepository.AddImageGalleryFromAdmin(addImage);
            }

            else
            {
                string imagepath = "";
                addImage.ImageName = "NoProductImage.png";
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img", addImage.ImageName);

                await using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

                ImageConvertor ImageResize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb", addImage.ImageName);

                ImageResize.Image_resize(imagepath, thumbPath, 200);

                addImage.IsDefault = model.IsDefault;
                addImage.ProductId = model.ProductId;
                addImage.IsDelete = false;
                addImage.CreatDate = DateTime.Now;
                return await _productRepository.AddImageGalleryFromAdmin(addImage);
            }


            return 0;
        }

        public Task<List<ProductGallery>> GetAllImageGalleryFromAdmin(int id)
        {
            return _productRepository.GetAllImageGalleryFromAdmin(id);
        }

        public async Task<ProductGallery> GetGalleryById(int id)
        {
            return await _productRepository.GetGalleryById(id);
        }

        public async Task<bool> DeleteImage(ProductGallery model)
        {


            if (model.ImageName != "NoProductImage.png")
            {
                string deletPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img", model.ImageName);
                if (File.Exists(deletPath))
                {
                    File.Delete(deletPath);
                }

                string deletThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb", model.ImageName);
                if (File.Exists(deletThumbPath))
                {
                    File.Delete(deletThumbPath);
                }

                return await _productRepository.DeleteImage(model);
            }

            return await _productRepository.DeleteImage(model); ;

        }

        public async Task<List<ProductComment>> GetAllCommentsByProductId(int id)
        {
            return await _productRepository.GetAllCommentsByProductId(id);
        }

        public async Task<bool> EditFeatureFromAdmin(EditFeatureViewModel model)
        {
            var getFeature = await _productRepository.GetFeatureById(model.Id);
            getFeature.Title=model.Title;
            getFeature.Type=model.Type;


            var res= await _productRepository.UpdateFeature(getFeature);
           
            return true;
        }

        public async Task<EditFeatureViewModel> GetFeatureById(int id)
        {
            var res = await _productRepository.GetFeatureById(id);

            var viewModel = new EditFeatureViewModel
            {
                Title = res.Title,
                Id = res.Id,
                Value = res.FeatureValues.FirstOrDefault(c => c.FeatureId == res.Id).Value,
                Type = res.Type
            };
            return viewModel;
        }

        public async Task<bool> DeleteFeature(int id)
        {
            return await _productRepository.DeleteFeature(id);
        }

        public async Task<int> AddFeatureValue(AddFeatureValueViewModel model)
        {
            var addFeatureValue = new FeatureValue()
            {
                FeatureId = model.FeatureId,
                CreatDate = DateTime.Now,
                IsDelete = false,
                Value = model.Title,

            };
            return await _productRepository.AddFeatureValueFromAdmin(addFeatureValue);
        }

        public async Task<List<FeatureValue>> GetAllValues(int featureId)
        {
            return await _productRepository.GetAllValues(featureId);
        }

        public async Task<EditFeatureValueViewModel> GetFeatureValueByIdForEdit(int id)
        {
            var res = await _productRepository.GetFeatureValueByIdForEdit(id);
            var viewModel = new EditFeatureValueViewModel()
            {
                FeatureId = res.FeatureId,
                Id = res.Id,
                Title = res.Value,
            };
            return viewModel;
        }

        public async Task<bool> UpdateFeatureValue(EditFeatureValueViewModel model)
        {
            var res = await _productRepository.GetFeatureValueByIdForEdit(model.Id);

            res.Value = model.Title;
            return await _productRepository.UpdateFeatureValue(res);
        }

        public async Task<bool> DeleteFeatureValue(EditFeatureValueViewModel model)
        {
            var res = await _productRepository.GetFeatureValueByIdForEdit(model.Id);

            return await _productRepository.DeleteFeatureValue(res);
        }

        public async Task<FilterProductCommentsViewModel> GetAllCommentOfProductForAdmin(FilterProductCommentsViewModel filter)
        {
            return await _productRepository.GetAllCommentsByProductForAdmin(filter);
        }

        public async Task<AnswerCommentViewModel> GetCommentForAdmin(int commentId)
        {
            var comment = await _productRepository.GetCommentById(commentId);
            var res = new AnswerCommentViewModel()
            {
                ProductId = comment.ProductId,
                CommentId = comment.Id,
                ParentId = comment.ParentId,
                SenderId = comment.SenderId,
                UserComment = comment.Comment,
                UserEmail = comment.User.Email,
                ProductName = comment.Product.Title,
                Answer = null
            };

            return res;
        }

        public async Task<bool> DeleteCommentByIdFromAdmin(int commentId)
        {
            return await _productRepository.DeleteCommentById(commentId);
        }

        public async Task<bool> AnswerCommentFromAdmin(AnswerCommentViewModel answerComment)
        {
            return await _productRepository.AnswerComment(answerComment);
        }

        public async Task<int> AddCommentFromUser(AddCommentViewModel model)
        {
            var addComment = new ProductComment()
            {
                Status = model.Status,
                Comment = model.Comment,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = model.ParentId,
                ProductId = model.ProductId,
                SenderId = model.SenderId,

            };

            return await _productRepository.AddCommentFromUser(addComment);
        }

        public async Task<string> GetDefaultImageById(int id)
        {
            return await _productRepository.GetDefaultImageById(id);
        }

        public Task<CommentViewModel> GetCommentsByUserId(CommentViewModel filter, int userId)
        {
            return _productRepository.GetCommentsByUserId(filter, userId);
        }

        public async Task<bool> DeleteCommentFromUser(int id)
        {
            var comment = await _productRepository.GetCommentById(id);
            return await _productRepository.DeleteCommentFromUser(comment);

        }

        public async Task<FavoriteProduct> AddFavoriteProduct(int prId, int UserId)
        {
            var favorite = new FavoriteProduct()
            {
                ProductId = prId,
                UserId = UserId,

                IsDelete = false,
                CreatDate = DateTime.Now,

            };
            return await _productRepository.AddFavoriteProduct(favorite);
        }

        public async Task<bool> DeleteFavoriteProduct(int id)
        {
            return await _productRepository.DeleteFavoriteProduct(id);
        }

        public async Task<FavoriteProductViewModel> GetFavoriteProductForAdmin(FavoriteProductViewModel model, int userId)
        {
            return await _productRepository.GetFavoriteProductForAdmin(model, userId);
        }

        public async Task<FilterProductByCategory> GetProductByCategorty(FilterProductByCategory filter, int categoryId)
        {
            return await _productRepository.GetProductByCategorty(filter, categoryId);
        }

        public async Task<FilterProductByCategory> GetProductByCategortyName(FilterProductByCategory filter, string productName)
        {
            return await _productRepository.GetProductByCategortyName(filter, productName);
        }

        public async Task<FeatureViewModel> GetAllFeatureForAdmin(FeatureViewModel model)
        {
            return await _productRepository.GetAllFeatureForAdmin(model);
        }

        public async Task<int> AddFeatureFromAdmin(AddFeatureViewModel model)
        {
            var addFeature = new Feature()
            {
                CreatDate = DateTime.Now,
                Title = model.Title,
                IsDelete = false,
                Type = model.Type

            };
            var FeatureId = await _productRepository.AddFeatureFromAdmin(addFeature);

            var valueModel = new FeatureValue()
            {
                CreatDate = DateTime.Now,
                Value = model.Value,
                IsDelete = false,
                FeatureId = addFeature.Id
            };
            var FeatureValue = await _productRepository.AddFeatureValueFromAdmin(valueModel);
            return FeatureId;
        }


        public async Task<EditOrAddCategoryProduct> GetCategoryByIdForAdmin(int id, int? parentId)
        {
            var res = await _productRepository.GetCategoryById(id, parentId);
            var model = new EditOrAddCategoryProduct
            {
                CategoryId = res.Id,
                CategoryName = res.Title,
                IsDeleted = res.IsDelete,
                ParentId = res.ParnetId
            };

            return model;
        }

        public async Task<List<Feature>> GetFeaturesForAddProduct()
        {

            var feature = await _productRepository.GetAllFetures();

            return feature;
        }

        public async Task<List<SelectListItem>> GetProductFeatureValuesByFeatureId(int featureId)
        {
            var res = await _productRepository.GetAllFeturesValuesById(featureId);


            return res.Select(f => new SelectListItem()
            {
                Value = f.Id.ToString(),
                Text = f.Value
            }).ToList();
        }

        public async Task<List<FeatureValue>> GetAllFeatureValues()
        {
            return await _productRepository.GetAllFeatureValues();
        }

        public async Task<List<ProductSelectedFeature>> GetAllProductFeatures(int productId)
        {
            var res = await _productRepository.GetAllFeaturesOfProduct(productId);
            return res;
        }

        public async Task<bool> AddFeatureToProduct(List<int> featureValues, int plusPrice, int productId)
        {
            int priceId = await _productRepository.AddProductPrice(plusPrice, productId);
            foreach (var item in featureValues.Where(c=>c!=0))
            {
                var feature = new ProductSelectedFeature()
                {
                    CreatDate = DateTime.Now,
                    FeatureValueId = item,
                    FeatureId =await _productRepository.GetFeatureIdByFeatureValueId(item),
                    ProductPriceId = priceId
                };
                bool status = await _productRepository.AddProductSelectedFeature(feature);
                if (status != true)
                {
                    return false;
                }

            }
            return true;
        }

        public async Task<List<ProductPrice>> GetAllPricesOfProduct(int productId)
        {
            return await _productRepository.GetAllPricesOfProduct(productId);
        }

        public async Task<List<ProductSelectedFeature>> GetAllFeaturesByProductPriceId(int priceId)
        {
            return await _productRepository.GetAllFeaturesByProductPriceId(priceId);
        }

        public async Task<List<ProductSelectedFeature>> GetAllFeaturesSelected(int productId)
        {
            return await _productRepository.GetFeaturesByProductId(productId);
        }

        public async Task<bool> DeleteFeatureFromProduct(int priceId)
        {
            try
            {
                await _productRepository.RemoveProductPrice(priceId);
                await _productRepository.RemoveFeatureFromProduct(priceId);
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }


}
