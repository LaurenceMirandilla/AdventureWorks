using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class LocationViewModel
    {
        public short LocationId { get; set; }
        public string Name { get; set; }
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
