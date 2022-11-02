using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class mig_reInitialiazed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    BannerCol = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DicountPercent = table.Column<int>(type: "int", nullable: false),
                    Useable = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParnetId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParnetId",
                        column: x => x.ParnetId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatForm = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPages_DynamicLinks_LinkId",
                        column: x => x.LinkId,
                        principalTable: "DynamicLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeatureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureValues_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Desctiption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsFinally = table.Column<bool>(type: "bit", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalizedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsReadByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsReadByOwner = table.Column<bool>(type: "bit", nullable: false),
                    Part = table.Column<int>(type: "int", nullable: false),
                    Levels = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDiscountCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDiscountCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDiscountCodes_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDiscountCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_ProductComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductComments_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGalleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGalleries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSelectedCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSelectedCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategories_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vote = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVotes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketMassages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMassages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMassages_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketMassages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vote = table.Column<bool>(type: "bit", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentVotes_ProductComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProductComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductPriceId = table.Column<int>(type: "int", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSelectedFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPriceId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    FeatureValueId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSelectedFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSelectedFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSelectedFeatures_FeatureValues_FeatureValueId",
                        column: x => x.FeatureValueId,
                        principalTable: "FeatureValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSelectedFeatures_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailProductFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailProductFeatures_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "ParentId", "Title" },
                values: new object[] { 1, new DateTime(2022, 11, 2, 20, 9, 16, 420, DateTimeKind.Local).AddTicks(9873), false, null, "مدیریت سایت" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatDate", "IsDelete", "RoleTitle" },
                values: new object[] { 1, new DateTime(2022, 11, 2, 20, 9, 16, 419, DateTimeKind.Local).AddTicks(656), false, "مدیریت اصلی سایت" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCode", "BirthDate", "CreatDate", "Email", "FirstName", "Gender", "IsAdmin", "IsDelete", "LastName", "Password", "PhoneNumber", "Status" },
                values: new object[] { 1, "123456", new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(6404), new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(4625), "yektakala@admin.com", null, 0, true, false, null, "20-2C-B9-62-AC-59-07-5B-96-4B-07-15-2D-23-4B-70", "12345678910", 0 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "ParentId", "Title" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1513), false, 1, "مدیریت کاربران" },
                    { 13, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2051), false, 1, "مدیریت تماس با ما" },
                    { 17, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2209), false, 1, "مدیریت تیکت ها" },
                    { 22, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2409), false, 1, "میدیریت محصولات" },
                    { 30, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2763), false, 1, "مدیریت ویژگی ها" },
                    { 32, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2864), false, 1, "مدیریت دسته بندی محصولات" },
                    { 35, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3096), false, 1, "مدیریت نقش ها" },
                    { 39, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3301), false, 1, "مدیریت لینک" },
                    { 46, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3653), false, 1, "مدیریت بنر" },
                    { 50, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3851), false, 1, " مدیریت صفحه های داینامیک" },
                    { 51, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3899), false, 1, " مدیریت سوالات متداول" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "PermissionId", "RoleId" },
                values: new object[] { 1, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(180), false, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatDate", "IsDelete", "RoleId", "UserId" },
                values: new object[] { 1, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(4526), false, 1, 1 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "ParentId", "Title" },
                values: new object[,]
                {
                    { 3, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1579), false, 2, "افزودن کاربر" },
                    { 38, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3251), false, 35, "حذف نقش" },
                    { 37, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3200), false, 35, "ویرایش نقش" },
                    { 36, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3150), false, 35, "افزودن نقش" },
                    { 42, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3453), false, 39, "حذف لینک" },
                    { 45, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3603), false, 32, "حذف دسته بندی" },
                    { 44, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3553), false, 32, "ویرایش دسته بندی" },
                    { 43, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3504), false, 32, "افزودن دسته بندی" },
                    { 31, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2814), false, 30, "مدیریت مقادیر ویژگی ها" },
                    { 47, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3702), false, 46, "افزودن بنر" },
                    { 29, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2713), false, 22, "حذف محصول" },
                    { 28, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2662), false, 22, "ویرایش محصول" },
                    { 27, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2614), false, 22, "مدیریت تگ های محصول" },
                    { 25, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2564), false, 22, "مدیریت نظرات محصول" },
                    { 41, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3404), false, 39, "ویرایش لینک" },
                    { 24, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2516), false, 22, "مدیریت تصاویر محصول" },
                    { 48, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3753), false, 46, "ویرایش بنر" },
                    { 21, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2360), false, 17, "پاسخ به تیکت" },
                    { 20, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2311), false, 17, "بستن تیکت" },
                    { 18, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2261), false, 17, "افزودن تیکت" },
                    { 49, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3803), false, 46, "حذف بنر" },
                    { 16, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2158), false, 13, "پاسخ تماس با ما" },
                    { 15, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2108), false, 13, "حذف تماس با ما" },
                    { 8, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1991), false, 2, "محصولات مورد علافه کاربر" },
                    { 7, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1809), false, 2, "دسترسی کاربر" },
                    { 6, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1755), false, 2, "سفارشات کاربران" },
                    { 5, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1689), false, 2, "حذف کاربر" },
                    { 4, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(1634), false, 2, "ویرایش کاربر" },
                    { 23, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2460), false, 22, "افزودن محصول" },
                    { 40, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3350), false, 39, "افزودن لینک" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 32, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3159), false, 39, 1 },
                    { 39, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3524), false, 46, 1 },
                    { 16, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2228), false, 22, 1 },
                    { 25, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2799), false, 32, 1 },
                    { 23, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2695), false, 30, 1 },
                    { 43, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3794), false, 50, 1 },
                    { 12, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2023), false, 17, 1 },
                    { 9, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1862), false, 13, 1 },
                    { 2, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1476), false, 2, 1 },
                    { 28, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2956), false, 35, 1 },
                    { 44, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3844), false, 51, 1 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "ParentId", "Title" },
                values: new object[,]
                {
                    { 33, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(2913), false, 7, "افزودن نقش کاربر" },
                    { 34, new DateTime(2022, 11, 2, 20, 9, 16, 421, DateTimeKind.Local).AddTicks(3035), false, 7, "حذف نقش کاربر" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 3, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1539), false, 3, 1 },
                    { 40, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3638), false, 47, 1 },
                    { 35, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3320), false, 42, 1 },
                    { 34, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3267), false, 41, 1 },
                    { 33, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3211), false, 40, 1 },
                    { 31, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3108), false, 38, 1 },
                    { 30, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3057), false, 37, 1 },
                    { 29, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3007), false, 36, 1 },
                    { 38, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3474), false, 45, 1 },
                    { 37, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3423), false, 44, 1 },
                    { 36, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3372), false, 43, 1 },
                    { 24, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2748), false, 31, 1 },
                    { 22, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2643), false, 29, 1 },
                    { 21, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2590), false, 28, 1 },
                    { 19, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2386), false, 25, 1 },
                    { 41, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3692), false, 48, 1 },
                    { 18, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2334), false, 24, 1 },
                    { 17, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2279), false, 23, 1 },
                    { 15, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2176), false, 21, 1 },
                    { 14, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2125), false, 20, 1 },
                    { 13, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2074), false, 18, 1 },
                    { 11, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1970), false, 16, 1 },
                    { 10, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1918), false, 15, 1 },
                    { 8, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1810), false, 8, 1 },
                    { 7, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1757), false, 7, 1 },
                    { 6, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1704), false, 6, 1 },
                    { 5, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1646), false, 5, 1 },
                    { 4, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(1593), false, 4, 1 },
                    { 20, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2528), false, 27, 1 },
                    { 42, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(3743), false, 49, 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "PermissionId", "RoleId" },
                values: new object[] { 26, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2851), false, 33, 1 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatDate", "IsDelete", "PermissionId", "RoleId" },
                values: new object[] { 27, new DateTime(2022, 11, 2, 20, 9, 16, 422, DateTimeKind.Local).AddTicks(2903), false, 34, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentId",
                table: "CommentVotes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_UserId",
                table: "CommentVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPages_LinkId",
                table: "DynamicPages",
                column: "LinkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_ProductId",
                table: "FavoriteProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_UserId",
                table: "FavoriteProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureValues_FeatureId",
                table: "FeatureValues",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailProductFeatures_OrderDetailId",
                table: "OrderDetailProductFeatures",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductPriceId",
                table: "OrderDetails",
                column: "ProductPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParnetId",
                table: "ProductCategories",
                column: "ParnetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ParentId",
                table: "ProductComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_SenderId",
                table: "ProductComments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategories_CategoryId",
                table: "ProductSelectedCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategories_ProductId",
                table: "ProductSelectedCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedFeatures_FeatureId",
                table: "ProductSelectedFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedFeatures_FeatureValueId",
                table: "ProductSelectedFeatures",
                column: "FeatureValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedFeatures_ProductPriceId",
                table: "ProductSelectedFeatures",
                column: "ProductPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVotes_ProductId",
                table: "ProductVotes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVotes_UserId",
                table: "ProductVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMassages_SenderId",
                table: "TicketMassages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMassages_TicketId",
                table: "TicketMassages",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OwnerId",
                table: "Tickets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscountCodes_DiscountId",
                table: "UserDiscountCodes",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscountCodes_UserId",
                table: "UserDiscountCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "CommentVotes");

            migrationBuilder.DropTable(
                name: "ContactUss");

            migrationBuilder.DropTable(
                name: "DynamicPages");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "OrderDetailProductFeatures");

            migrationBuilder.DropTable(
                name: "ProductGalleries");

            migrationBuilder.DropTable(
                name: "ProductSelectedCategories");

            migrationBuilder.DropTable(
                name: "ProductSelectedFeatures");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "ProductVotes");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "TicketMassages");

            migrationBuilder.DropTable(
                name: "UserDiscountCodes");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "DynamicLinks");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "FeatureValues");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
