using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class BillOfMaterialsDTO
    {
        public int BillOfMaterialsId { get; set; }

        public int? ProductAssemblyId { get; set; }

        [Required]
        public int ComponentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int BOMLevel { get; set; }

        public decimal perassemblyqty { get; set; }

        public DateTime ModifiedDate { get; set; }

        // unit measure code must be provided by caller
        [Required, StringLength(3)]
        public string UnitMeasureCode { get; set; } = null!;
    }
}
