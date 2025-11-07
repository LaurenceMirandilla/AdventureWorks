using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class WorkOrderDTO
    {
        public int WorkOrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(0, 99999)]
        public int OrderQty { get; set; }

        [Range(0, 99999)]
        public int StockedQty { get; set; }

        [Range(0, double.MaxValue)]
        public short ScrappedQty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime DueDate { get; set; }

        public short? ScrapReasonId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
