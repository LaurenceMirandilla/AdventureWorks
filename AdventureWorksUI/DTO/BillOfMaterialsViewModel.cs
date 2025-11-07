namespace AdventureWorks.UI.Models
{
    public class BillOfMaterialsViewModel
    {
        public int BillOfMaterialsId { get; set; }
        public int? ProductAssemblyId { get; set; }
        public int ComponentId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public decimal PerAssemblyQty { get; set; }
        public string UnitMeasureCode { get; set; } = string.Empty;
        public int? BomLevel { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}