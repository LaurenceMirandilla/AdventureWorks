using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class WorkOrderRoutingViewModel
    {
        public int ProductId { get; set; }
        [Key]
        public short OperationSequence { get; set; }
        public int WorkOrderId { get; set; }
        public short LocationId { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? ActualResourceHrs { get; set; }
        public decimal PlannedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
