using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductModelProductDescriptionCultureDTO
    {
        [Required]
        public int ProductModelId { get; set; }

        [Required]
        public int ProductDescriptionId { get; set; }

        [Required, StringLength(6)]
        public string CultureId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
