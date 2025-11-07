using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductDescriptionViewModel
    {
        public int ProductDescriptionId { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
