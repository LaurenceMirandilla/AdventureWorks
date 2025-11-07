using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class TransactionHistoryArchiveDTO
    {
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ReferenceOrderId { get; set; }

        [Required]
        public int ReferenceOrderLineId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required, StringLength(1)]
        public string TransactionType { get; set; }

        [Range(0, 99999)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ActualCost { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
