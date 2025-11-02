using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class UnitMeasureDTO
    {
        [Required, StringLength(3)]
        public string UnitMeasureCode { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

