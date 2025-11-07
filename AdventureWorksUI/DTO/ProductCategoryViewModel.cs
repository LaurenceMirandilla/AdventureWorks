using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductCategoryViewModel
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
