namespace AdventureWorks.Model.Domain.Production
{
    public class Product
    {
        public Product()
        {
            BillOfMaterials = new HashSet<BillOfMaterials>();
            ProductCostHistory = new HashSet<ProductCostHistory>();
            ProductInventories = new HashSet<ProductInventory>();
            ProductListPriceHistory = new HashSet<ProductListPriceHistory>();
            ProductProductPhotos = new HashSet<ProductProductPhoto>();
            ProductReviews = new HashSet<ProductReview>();
            TransactionHistories = new HashSet<TransactionHistory>();
            WorkOrderRoutings = new HashSet<WorkOrderRouting>();
            WorkOrders = new HashSet<WorkOrder>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string? Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public int? ProductSubcategoryId { get; set; }
        public int? ProductModelId { get; set; }
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductModel ProductModel { get; set; }
        public virtual ICollection<BillOfMaterials> BillOfMaterials { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistory { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<ProductListPriceHistory> ProductListPriceHistory { get; set; }
        public virtual ICollection<ProductProductPhoto> ProductProductPhotos { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
        public virtual ICollection<WorkOrderRouting> WorkOrderRoutings { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}

