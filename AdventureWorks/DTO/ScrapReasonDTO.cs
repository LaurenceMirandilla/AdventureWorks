using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ScrapReasonDTO
    {
        public short ScrapReasonId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
