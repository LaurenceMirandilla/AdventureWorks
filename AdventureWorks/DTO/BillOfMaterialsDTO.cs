using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class BillOfMaterialsDTO
    {
        [Required]
        public int BillOfMaterialsId { get; set; }

        [Required]
        public int? ProductAssemblyId { get; set; }

        [Required]
        public int ComponentId { get; set; }

        [Range(1, 9999)]
        public int PerAssemblyQty { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
