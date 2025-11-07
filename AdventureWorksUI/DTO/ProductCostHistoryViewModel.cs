using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductCostHistoryViewModel
    {
        public int ProductCostHistoryId { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal StandardCost { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
