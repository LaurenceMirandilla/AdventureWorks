using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductInventoryViewModel
    {
        public int ProductInventoryId { get; set; }
        public int ProductId { get; set; }
        public short LocationId { get; set; }
        public string Shelf { get; set; }
        public byte Bin { get; set; }
        public short Quantity { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ProductViewModel Product { get; set; }

    }
}
