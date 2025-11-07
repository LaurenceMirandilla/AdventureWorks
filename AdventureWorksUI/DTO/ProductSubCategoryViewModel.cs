using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductSubCategoryViewModel
    {
        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; } = null!;

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
