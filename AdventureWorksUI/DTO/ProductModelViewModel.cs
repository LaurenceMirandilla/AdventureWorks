using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductModelViewModel
    {
        public int ProductModelId { get; set; }
        public string Name { get; set; }
        public string? CatalogDescription { get; set; }
        public string? Instructions { get; set; }

        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
