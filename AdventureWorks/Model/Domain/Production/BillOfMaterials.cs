using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorks.Model.Domain.Production
{
    public class BillOfMaterials
    {
        public int BillOfMaterialsId { get; set; }
        public int? ProductAssemblyId { get; set; }
        public int ComponentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int BOMLevel { get; set; }
        public decimal perassemblyqty { get; set; }
        public DateTime ModifiedDate { get; set; }

        // <-- new FK property pointing to UnitMeasure.UnitMeasureCode (string)
        public string UnitMeasureCode { get; set; } = null!;

        // navigation properties
        [ForeignKey(nameof(ProductAssemblyId))]
        public virtual Product ProductAssembly { get; set; }

        // explicitly tell EF this navigation uses UnitMeasureCode
        [ForeignKey(nameof(UnitMeasureCode))]
        public virtual UnitMeasure UnitMeasure { get; set; }
    }
}
