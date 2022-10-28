using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Banner;
using Domain.Models.Order;
using Domain.Models.Product;
using Domain.Models.Tickets;
using Domain.Models.Common;
using Domain.Models.Enums;
using Domain.Models.Role;
using Domain.Models.FAQ;
using Domain.Models.Votes;
using Domain.ViewModels.Order;
using Domain.Models.ContactUss;
using Domain.Models.UserAgg;

namespace DataLayer.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ContactUss> ContactUss { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMassages> TicketMassages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureValue> FeatureValues { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductSelectedFeature> ProductSelectedFeatures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailProductFeature> OrderDetailProductFeatures { get; set; }
        public DbSet<DynamicLink> DynamicLinks { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<DynamicPage> DynamicPages { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ProductVotes> ProductVotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<Discount> Discounts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<ProductCategory>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<TicketMassages>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Ticket>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<ContactUss>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<ProductTag>()
                .HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<ProductGallery>()
                .HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<ProductSelectedCategory>()
                .HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<ProductComment>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<FavoriteProduct>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Feature>()
                .HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<FeatureValue>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<ProductPrice>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<ProductSelectedFeature>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Order>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<OrderDetail>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<DynamicLink>()
                .HasQueryFilter(c => !c.IsDelete);
            
            modelBuilder.Entity<SocialMedia>()
                .HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Permission>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<RolePermission>()
                .HasQueryFilter(c => !c.IsDelete);


            modelBuilder.Entity<UserDiscountCode>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<UserRoles>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<FAQ>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<DynamicPage>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Banner>()
                .HasQueryFilter(c => !c.IsDelete);
            
            modelBuilder.Entity<Discount>()
                .HasQueryFilter(c => !c.IsDelete);

            #region Seed Data

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                CreatDate = DateTime.Now,
                IsDelete = false,
                RoleTitle = "مدیریت اصلی سایت"
            });

            modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 1,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = null,
                Title = "مدیریت سایت"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 2,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت کاربران"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 3,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 2,
                Title = "افزودن کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 4,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 2,
                Title = "ویرایش کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 5,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 2,
                Title = "حذف کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 6,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 2,
                Title = "سفارشات کاربران"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 7,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 2,
                Title = "دسترسی کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 8,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 2,
                Title = "محصولات مورد علافه کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 13,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت تماس با ما"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 15,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 13,
                Title = "حذف تماس با ما"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 16,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 13,
                Title = "پاسخ تماس با ما"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 17,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت تیکت ها"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 18,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 17,
                Title = "افزودن تیکت"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 20,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 17,
                Title = "بستن تیکت"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 21,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 17,
                Title = "پاسخ به تیکت"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 22,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "میدیریت محصولات"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 23,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 22,
                Title = "افزودن محصول"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 24,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 22,
                Title = "مدیریت تصاویر محصول"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 25,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 22,
                Title = "مدیریت نظرات محصول"
            });
            modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 27,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 22,
                Title = "مدیریت تگ های محصول"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 28,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 22,
                Title = "ویرایش محصول"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 29,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 22,
                Title = "حذف محصول"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 30,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت ویژگی ها"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 31,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 30,
                Title = "مدیریت مقادیر ویژگی ها"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 32,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت دسته بندی محصولات"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 33,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 7,
                Title = "افزودن نقش کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 34,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 7,
                Title = "حذف نقش کاربر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 35,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت نقش ها"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 36,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 35,
                Title = "افزودن نقش"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 37,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 35,
                Title = "ویرایش نقش"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 38,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 35,
                Title = "حذف نقش"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 39,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت لینک"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 40,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 39,
                Title = "افزودن لینک"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 41,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 39,
                Title = "ویرایش لینک"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 42,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 39,
                Title = "حذف لینک"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 43,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 32,
                Title = "افزودن دسته بندی"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 44,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 32,
                Title = "ویرایش دسته بندی"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 45,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 32,
                Title = "حذف دسته بندی"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 46,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = "مدیریت بنر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 47,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 46,
                Title = "افزودن بنر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 48,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 46,
                Title = "ویرایش بنر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 49,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 46,
                Title = "حذف بنر"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 50,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = " مدیریت صفحه های داینامیک"
            }); modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                Id = 51,
                CreatDate = DateTime.Now,
                IsDelete = false,
                ParentId = 1,
                Title = " مدیریت سوالات متداول"
            });


            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                CreatDate = DateTime.Now,
                IsDelete = false,
                Email = "yektakala@admin.com",
                Password = "20-2C-B9-62-AC-59-07-5B-96-4B-07-15-2D-23-4B-70",
                ActiveCode = "123456",
                BirthDate = DateTime.Now,
                Gender = Gender.male,
                IsAdmin = true,
                PhoneNumber = "12345678910",
                Status = Status.Active,

            });



            modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 1,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 1,
                RoleId = 1
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 2,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 2,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 3,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 3,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 4,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 4,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 5,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 5,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 6,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 6,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 7,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 7,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 8,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 8,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 9,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 13,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 10,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 15,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 11,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 16,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 12,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 17,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 13,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 18,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 14,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 20,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 15,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 21,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 16,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 22,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 17,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 23,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 18,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 24,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 19,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 25,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 20,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 27,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 21,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 28,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 22,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 29,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 23,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 30,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 24,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 31,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 25,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 32,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 26,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 33,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 27,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 34,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 28,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 35,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 29,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 36,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 30,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 37,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 31,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 38,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 32,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 39,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 33,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 40,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 34,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 41,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 35,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 42,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 36,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 43,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 37,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 44,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 38,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 45,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 39,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 46,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 40,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 47,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 41,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 48,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 42,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 49,
                RoleId = 1,
            }); modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 43,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 50,
                RoleId = 1,
            });
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                Id = 44,
                CreatDate = DateTime.Now,
                IsDelete = false,
                PermissionId = 51,
                RoleId = 1,
            });

            modelBuilder.Entity<UserRoles>().HasData(new UserRoles()
            {
                Id = 1,
                CreatDate = DateTime.Now,
                UserId = 1,
                RoleId = 1,
                IsDelete = false,
            });

            #endregion
        }

    }
}
