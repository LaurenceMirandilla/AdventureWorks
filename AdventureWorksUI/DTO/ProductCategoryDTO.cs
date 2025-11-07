using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductCategoryDTO
    {
        public int ProductCategoryId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
