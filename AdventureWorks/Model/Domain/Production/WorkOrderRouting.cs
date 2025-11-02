namespace AdventureWorks.Model.Domain.Production


{
    public partial class WorkOrderRouting
    {
        public int WorkOrderId { get; set; }
        public int ProductId { get; set; }
        public short OperationSequence { get; set; }
        public short LocationId { get; set; }
        public decimal ScheduledStartDate { get; set; }
        public decimal ScheduledEndDate { get; set; }
        public decimal? ActualStartDate { get; set; }
        public decimal? ActualEndDate { get; set; }
        public decimal? ActualResourceHrs { get; set; }
        public decimal PlannedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
