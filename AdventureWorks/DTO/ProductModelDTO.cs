using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductModelDTO
    {
        public int ProductModelId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public string? CatalogDescription { get; set; }
        public string? Instructions { get; set; }

        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
