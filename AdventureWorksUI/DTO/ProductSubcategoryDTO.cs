using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductSubcategoryDTO
    {
        public int ProductSubcategoryId { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = null!;

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
