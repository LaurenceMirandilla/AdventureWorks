using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO

{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(25)]
        public string ProductNumber { get; set; }

        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }

        [StringLength(15)]
        public string? Color { get; set; }

        [Range(0, double.MaxValue)]
        public decimal StandardCost { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ListPrice { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }

        [StringLength(3)]
        public string? SizeUnitMeasureCode { get; set; }

        [StringLength(3)]
        public string? WeightUnitMeasureCode { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Weight { get; set; }

        [Range(0, 9999)]
        public int DaysToManufacture { get; set; }

        [StringLength(2)]
        public string? ProductLine { get; set; }

        [StringLength(2)]
        public string? Class { get; set; }

        [StringLength(2)]
        public string? Style { get; set; }

        public int? ProductSubcategoryId { get; set; }
        public int? ProductModelId { get; set; }

        [Required]
        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
    }
}
