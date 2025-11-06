using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Implementations;
using AdventureWorks.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddScoped<IProductSubcategoryRepository, ProductSubcategoryRepository>();
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

        // Configure Swagger with Bearer JWT support so "Authorize" appears in Swagger UI
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventureWorks API", Version = "v1" });

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Enter JWT token. Example: \"eyJhbGci...\" (no 'Bearer ' prefix required)",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, new string[] { } }
            });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

        // Authorization must be registered before Build()`
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();
        app.UseAuthentication(); // must be before UseAuthorization
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}

