using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Model.Domain.Production


{
    public class UnitMeasure
    {
        [Key]
        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
