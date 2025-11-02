using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductInventoryDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public short LocationId { get; set; }

        [Required, StringLength(10)]
        public string Shelf { get; set; }

        [Range(0, 255)]
        public byte Bin { get; set; }

        [Range(0, 9999)]
        public short Quantity { get; set; }

        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

