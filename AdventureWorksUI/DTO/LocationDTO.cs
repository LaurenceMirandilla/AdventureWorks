using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class LocationDTO
    {
        public short LocationId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CostRate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Availability { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
