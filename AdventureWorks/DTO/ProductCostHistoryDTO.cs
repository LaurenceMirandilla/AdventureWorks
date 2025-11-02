using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductCostHistoryDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal StandardCost { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

