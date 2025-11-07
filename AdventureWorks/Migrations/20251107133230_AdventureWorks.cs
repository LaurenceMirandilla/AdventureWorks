using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorks.Migrations
{
    /// <inheritdoc />
    public partial class AdventureWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Production");

            migrationBuilder.CreateTable(
                name: "Culture",
                schema: "Production",
                columns: table => new
                {
                    CultureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture", x => x.CultureId);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Production",
                columns: table => new
                {
                    DocumentNode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revision = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentNode);
                });

            migrationBuilder.CreateTable(
                name: "Illustration",
                schema: "Production",
                columns: table => new
                {
                    IllustrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illustration", x => x.IllustrationId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "Production",
                columns: table => new
                {
                    LocationId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Availability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "Production",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductDescription",
                schema: "Production",
                columns: table => new
                {
                    ProductDescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescription", x => x.ProductDescriptionId);
                });

            migrationBuilder.CreateTable(
                name: "ProductModel",
                schema: "Production",
                columns: table => new
                {
                    ProductModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModel", x => x.ProductModelId);
                });

            migrationBuilder.CreateTable(
                name: "ProductPhoto",
                schema: "Production",
                columns: table => new
                {
                    ProductPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumbNailPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailPhotoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LargePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LargePhotoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhoto", x => x.ProductPhotoId);
                });

            migrationBuilder.CreateTable(
                name: "ScrapReason",
                schema: "Production",
                columns: table => new
                {
                    ScrapReasonId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapReason", x => x.ScrapReasonId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistoryArchive",
                schema: "Production",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReferenceOrderId = table.Column<int>(type: "int", nullable: false),
                    ReferenceOrderLineId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistoryArchive", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasure",
                schema: "Production",
                columns: table => new
                {
                    UnitMeasureCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasure", x => x.UnitMeasureCode);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubcategory",
                schema: "Production",
                columns: table => new
                {
                    ProductSubcategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubcategory", x => x.ProductSubcategoryId);
                    table.ForeignKey(
                        name: "FK_ProductSubcategory_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalSchema: "Production",
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductModelIllustration",
                schema: "Production",
                columns: table => new
                {
                    ProductModelIllustrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductModelId = table.Column<int>(type: "int", nullable: false),
                    IllustrationId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelIllustration", x => x.ProductModelIllustrationId);
                    table.ForeignKey(
                        name: "FK_ProductModelIllustration_Illustration_IllustrationId",
                        column: x => x.IllustrationId,
                        principalSchema: "Production",
                        principalTable: "Illustration",
                        principalColumn: "IllustrationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductModelIllustration_ProductModel_ProductModelId",
                        column: x => x.ProductModelId,
                        principalSchema: "Production",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductModelProductDescriptionCulture",
                schema: "Production",
                columns: table => new
                {
                    ProductModelProductDescriptionCultureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductModelId = table.Column<int>(type: "int", nullable: false),
                    ProductDescriptionId = table.Column<int>(type: "int", nullable: false),
                    CultureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelProductDescriptionCulture", x => x.ProductModelProductDescriptionCultureId);
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescriptionCulture_Culture_CultureId",
                        column: x => x.CultureId,
                        principalSchema: "Production",
                        principalTable: "Culture",
                        principalColumn: "CultureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionId",
                        column: x => x.ProductDescriptionId,
                        principalSchema: "Production",
                        principalTable: "ProductDescription",
                        principalColumn: "ProductDescriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelId",
                        column: x => x.ProductModelId,
                        principalSchema: "Production",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Production",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakeFlag = table.Column<bool>(type: "bit", nullable: false),
                    FinishedGoodsFlag = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SizeUnitMeasureCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightUnitMeasureCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaysToManufacture = table.Column<int>(type: "int", nullable: false),
                    ProductLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSubcategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductModelId = table.Column<int>(type: "int", nullable: true),
                    SellStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscontinuedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_ProductModel_ProductModelId",
                        column: x => x.ProductModelId,
                        principalSchema: "Production",
                        principalTable: "ProductModel",
                        principalColumn: "ProductModelId");
                    table.ForeignKey(
                        name: "FK_Product_ProductSubcategory_ProductSubcategoryId",
                        column: x => x.ProductSubcategoryId,
                        principalSchema: "Production",
                        principalTable: "ProductSubcategory",
                        principalColumn: "ProductSubcategoryId");
                });

            migrationBuilder.CreateTable(
                name: "BillOfMaterials",
                schema: "Production",
                columns: table => new
                {
                    BillOfMaterialsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAssemblyId = table.Column<int>(type: "int", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BOMLevel = table.Column<int>(type: "int", nullable: false),
                    perassemblyqty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnitMeasureCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillOfMaterials", x => x.BillOfMaterialsId);
                    table.ForeignKey(
                        name: "FK_BillOfMaterials_Product_ProductAssemblyId",
                        column: x => x.ProductAssemblyId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_BillOfMaterials_UnitMeasure_UnitMeasureCode",
                        column: x => x.UnitMeasureCode,
                        principalSchema: "Production",
                        principalTable: "UnitMeasure",
                        principalColumn: "UnitMeasureCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCostHistory",
                schema: "Production",
                columns: table => new
                {
                    ProductCostHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StandardCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCostHistory", x => x.ProductCostHistoryId);
                    table.ForeignKey(
                        name: "FK_ProductCostHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDocument",
                schema: "Production",
                columns: table => new
                {
                    ProductDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DocumentNode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentNodeNavigationDocumentNode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocument", x => x.ProductDocumentId);
                    table.ForeignKey(
                        name: "FK_ProductDocument_Document_DocumentNodeNavigationDocumentNode",
                        column: x => x.DocumentNodeNavigationDocumentNode,
                        principalSchema: "Production",
                        principalTable: "Document",
                        principalColumn: "DocumentNode");
                    table.ForeignKey(
                        name: "FK_ProductDocument_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventory",
                schema: "Production",
                columns: table => new
                {
                    ProductInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<short>(type: "smallint", nullable: false),
                    Shelf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bin = table.Column<byte>(type: "tinyint", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    Rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventory", x => x.ProductInventoryId);
                    table.ForeignKey(
                        name: "FK_ProductInventory_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Production",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductListPriceHistory",
                schema: "Production",
                columns: table => new
                {
                    ProductListPriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductListPriceHistory", x => x.ProductListPriceHistoryId);
                    table.ForeignKey(
                        name: "FK_ProductListPriceHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductPhoto",
                schema: "Production",
                columns: table => new
                {
                    ProductProductPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductPhotoId = table.Column<int>(type: "int", nullable: false),
                    Primary = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductPhoto", x => x.ProductProductPhotoId);
                    table.ForeignKey(
                        name: "FK_ProductProductPhoto_ProductPhoto_ProductPhotoId",
                        column: x => x.ProductPhotoId,
                        principalSchema: "Production",
                        principalTable: "ProductPhoto",
                        principalColumn: "ProductPhotoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductPhoto_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReview",
                schema: "Production",
                columns: table => new
                {
                    ProductReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.ProductReviewId);
                    table.ForeignKey(
                        name: "FK_ProductReview_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                schema: "Production",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReferenceOrderId = table.Column<int>(type: "int", nullable: false),
                    ReferenceOrderLineId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrder",
                schema: "Production",
                columns: table => new
                {
                    WorkOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderQty = table.Column<int>(type: "int", nullable: false),
                    StockedQty = table.Column<int>(type: "int", nullable: false),
                    ScrappedQty = table.Column<int>(type: "int", nullable: false),
                    ScrapReasonId = table.Column<short>(type: "smallint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrder", x => x.WorkOrderId);
                    table.ForeignKey(
                        name: "FK_WorkOrder_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrder_ScrapReason_ScrapReasonId",
                        column: x => x.ScrapReasonId,
                        principalSchema: "Production",
                        principalTable: "ScrapReason",
                        principalColumn: "ScrapReasonId");
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderRouting",
                schema: "Production",
                columns: table => new
                {
                    OperationSequence = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<short>(type: "smallint", nullable: false),
                    ScheduledStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualResourceHrs = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlannedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderRouting", x => x.OperationSequence);
                    table.ForeignKey(
                        name: "FK_WorkOrderRouting_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Production",
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderRouting_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Production",
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderRouting_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "Production",
                        principalTable: "WorkOrder",
                        principalColumn: "WorkOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterials_ProductAssemblyId",
                schema: "Production",
                table: "BillOfMaterials",
                column: "ProductAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_BillOfMaterials_UnitMeasureCode",
                schema: "Production",
                table: "BillOfMaterials",
                column: "UnitMeasureCode");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductModelId",
                schema: "Production",
                table: "Product",
                column: "ProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubcategoryId",
                schema: "Production",
                table: "Product",
                column: "ProductSubcategoryId",
                unique: true,
                filter: "[ProductSubcategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCostHistory_ProductId",
                schema: "Production",
                table: "ProductCostHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocument_DocumentNodeNavigationDocumentNode",
                schema: "Production",
                table: "ProductDocument",
                column: "DocumentNodeNavigationDocumentNode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocument_ProductId",
                schema: "Production",
                table: "ProductDocument",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_LocationId",
                schema: "Production",
                table: "ProductInventory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_ProductId",
                schema: "Production",
                table: "ProductInventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListPriceHistory_ProductId",
                schema: "Production",
                table: "ProductListPriceHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelIllustration_IllustrationId",
                schema: "Production",
                table: "ProductModelIllustration",
                column: "IllustrationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelIllustration_ProductModelId",
                schema: "Production",
                table: "ProductModelIllustration",
                column: "ProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescriptionCulture_CultureId",
                schema: "Production",
                table: "ProductModelProductDescriptionCulture",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescriptionCulture_ProductDescriptionId",
                schema: "Production",
                table: "ProductModelProductDescriptionCulture",
                column: "ProductDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelProductDescriptionCulture_ProductModelId",
                schema: "Production",
                table: "ProductModelProductDescriptionCulture",
                column: "ProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductPhoto_ProductId",
                schema: "Production",
                table: "ProductProductPhoto",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductPhoto_ProductPhotoId",
                schema: "Production",
                table: "ProductProductPhoto",
                column: "ProductPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                schema: "Production",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategory_ProductCategoryId",
                schema: "Production",
                table: "ProductSubcategory",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ProductId",
                schema: "Production",
                table: "TransactionHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ProductId",
                schema: "Production",
                table: "WorkOrder",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ScrapReasonId",
                schema: "Production",
                table: "WorkOrder",
                column: "ScrapReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderRouting_LocationId",
                schema: "Production",
                table: "WorkOrderRouting",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderRouting_ProductId",
                schema: "Production",
                table: "WorkOrderRouting",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderRouting_WorkOrderId",
                schema: "Production",
                table: "WorkOrderRouting",
                column: "WorkOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillOfMaterials",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductCostHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductDocument",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductInventory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductListPriceHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductModelIllustration",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductModelProductDescriptionCulture",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductProductPhoto",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductReview",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "TransactionHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "TransactionHistoryArchive",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "WorkOrderRouting",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "UnitMeasure",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Illustration",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Culture",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductDescription",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductPhoto",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "WorkOrder",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ScrapReason",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductModel",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductSubcategory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "Production");
        }
    }
}
