using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Implementations;
using AdventureWorks.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<AdventureWorksContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorks")));


        var mapperConfig = new MapperConfiguration(cfg =>
        {
            
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            cfg.CreateMap<ProductCostHistory, ProductCostHistoryDTO>().ReverseMap();
            cfg.CreateMap<ProductDescription, ProductDescriptionDTO>().ReverseMap();
            cfg.CreateMap<ProductDocument, ProductDocumentDTO>().ReverseMap();
            cfg.CreateMap<ProductInventory, ProductInventoryDTO>().ReverseMap();
            cfg.CreateMap<ProductListPriceHistory, ProductListPriceHistoryDTO>().ReverseMap();
            cfg.CreateMap<ProductModel, ProductModelDTO>().ReverseMap();
            cfg.CreateMap<ProductModelIllustration, ProductModelIllustrationDTO>().ReverseMap();
            cfg.CreateMap<ProductModelProductDescriptionCulture, ProductModelProductDescriptionCultureDTO>().ReverseMap();
            cfg.CreateMap<ProductPhoto, ProductPhotoDTO>().ReverseMap();
            cfg.CreateMap<ProductReview, ProductReviewDTO>().ReverseMap();
            cfg.CreateMap<ProductSubcategory, ProductSubcategoryDTO>().ReverseMap();
            cfg.CreateMap<TransactionHistory, TransactionHistoryDTO>().ReverseMap();
            cfg.CreateMap<TransactionHistoryArchive, TransactionHistoryArchiveDTO>().ReverseMap();
            cfg.CreateMap<UnitMeasure, UnitMeasureDTO>().ReverseMap();
            cfg.CreateMap<WorkOrder, WorkOrderDTO>().ReverseMap();
            cfg.CreateMap<WorkOrderRouting, WorkOrderRoutingDTO>().ReverseMap();
            cfg.CreateMap<ScrapReason, ScrapReasonDTO>().ReverseMap();
            cfg.CreateMap<BillOfMaterials, BillOfMaterialsDTO>().ReverseMap();
            cfg.CreateMap<Culture, CultureDTO>().ReverseMap();
            cfg.CreateMap<Document, DocumentDTO>().ReverseMap();
            cfg.CreateMap<Illustration, IllustrationDTO>().ReverseMap();
            cfg.CreateMap<Location, LocationDTO>().ReverseMap();
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        builder.Services.AddScoped<IProductDescriptionRepository, ProductDescriptionRepository>();
        builder.Services.AddScoped<IProductDocumentRepository, ProductDocumentRepository>();
        builder.Services.AddScoped<IProductInventoryRepository, ProductInventoryRepository>();
        builder.Services.AddScoped<IProductListPriceHistoryRepository, ProductListPriceHistoryRepository>();
        builder.Services.AddScoped<IProductModelRepository, ProductModelRepository>();
        builder.Services.AddScoped<IProductModelIllustrationRepository, ProductModelIllustrationRepository>();
        builder.Services.AddScoped<IProductModelProductDescriptionCultureRepository, ProductModelProductDescriptionCultureRepository>();
        builder.Services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
        builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
        builder.Services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
        builder.Services.AddScoped<ITransactionHistoryArchiveRepository, TransactionHistoryArchiveRepository>();
        builder.Services.AddScoped<IUnitMeasureRepository, UnitMeasureRepository>();
        builder.Services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
        builder.Services.AddScoped<IWorkOrderRoutingRepository, WorkOrderRoutingRepository>();
        builder.Services.AddScoped<IScrapReasonRepository, ScrapReasonRepository>();
        builder.Services.AddScoped<IBillOfMaterialsRepository, BillOfMaterialsRepository>();
        builder.Services.AddScoped<ICultureRepository, CultureRepository>();
        builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
        builder.Services.AddScoped<IIllustrationRepository, IllustrationRepository>();
        builder.Services.AddScoped<ILocationRepository, LocationRepository>();


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}