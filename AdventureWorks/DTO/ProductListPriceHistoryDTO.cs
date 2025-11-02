using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductListPriceHistoryDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ListPrice { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
