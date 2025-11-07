using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductDocumentViewModel
    {
        public int ProductId { get; set; }
        public string DocumentNode { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
