using Microsoft.EntityFrameworkCore;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
            : base(options)
        {
        }

        ///// PRODUCTION SCHEMA - DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCostHistory> ProductCostHistories { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelIllustration> ProductModelIllustrations { get; set; }
        public DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<TransactionHistoryArchive> TransactionHistoryArchives { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderRouting> WorkOrderRoutings { get; set; }
        public DbSet<ScrapReason> ScrapReasons { get; set; }
        public DbSet<BillOfMaterials> BillOfMaterials { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Illustration> Illustrations { get; set; }
        public DbSet<Location> Locations { get; set; }
        /// MODEL CONFIGURATION
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use the "Production" schema for all entities
            modelBuilder.HasDefaultSchema("Production");

            // Configure table names explicitly (optional)
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductCostHistory>().ToTable("ProductCostHistory");
            modelBuilder.Entity<ProductDescription>().ToTable("ProductDescription");
            modelBuilder.Entity<ProductDocument>().ToTable("ProductDocument");
            modelBuilder.Entity<ProductInventory>().ToTable("ProductInventory");
            modelBuilder.Entity<ProductListPriceHistory>().ToTable("ProductListPriceHistory");
            modelBuilder.Entity<ProductModel>().ToTable("ProductModel");
            modelBuilder.Entity<ProductModelIllustration>().ToTable("ProductModelIllustration");
            modelBuilder.Entity<ProductModelProductDescriptionCulture>().ToTable("ProductModelProductDescriptionCulture");
            modelBuilder.Entity<ProductPhoto>().ToTable("ProductPhoto");
            modelBuilder.Entity<ProductReview>().ToTable("ProductReview");
            modelBuilder.Entity<TransactionHistory>().ToTable("TransactionHistory");
            modelBuilder.Entity<TransactionHistoryArchive>().ToTable("TransactionHistoryArchive");
            modelBuilder.Entity<UnitMeasure>().ToTable("UnitMeasure");
            modelBuilder.Entity<WorkOrder>().ToTable("WorkOrder");
            modelBuilder.Entity<WorkOrderRouting>().ToTable("WorkOrderRouting");
            modelBuilder.Entity<WorkOrderRouting>()
            .HasOne(wr => wr.WorkOrder)
            .WithMany(w => w.WorkOrderRoutings)
            .HasForeignKey(wr => wr.WorkOrderId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ScrapReason>().ToTable("ScrapReason");
            modelBuilder.Entity<BillOfMaterials>().ToTable("BillOfMaterials");
            modelBuilder.Entity<Culture>().ToTable("Culture");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<Illustration>().ToTable("Illustration");
            modelBuilder.Entity<Location>().ToTable("Location");

            base.OnModelCreating(modelBuilder);
        }
    }
}
