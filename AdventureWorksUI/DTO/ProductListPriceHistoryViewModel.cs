using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductListPriceHistoryViewModel
    {
        public int ProductListPriceHistoryId { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public decimal ListPrice { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
