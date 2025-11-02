using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductDocumentDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required, StringLength(50)]
        public string DocumentNode { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
