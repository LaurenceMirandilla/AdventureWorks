using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
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
