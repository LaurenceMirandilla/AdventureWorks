using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class WorkOrderRoutingDTO
    {
        [Required]
        public int WorkOrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public short OperationSequence { get; set; }

        [Required]
        public short LocationId { get; set; }

        public DateTime ScheduledStartDate { get; set; }

        public DateTime ScheduledEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualResourceHrs { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PlannedCost { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualCost { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
