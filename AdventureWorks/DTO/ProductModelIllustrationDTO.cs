using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductModelIllustrationDTO
    {
        [Required]
        public int ProductModelId { get; set; }

        [Required]
        public int IllustrationId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
