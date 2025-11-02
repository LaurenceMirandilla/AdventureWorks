using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductDescriptionDTO
    {
        public int ProductDescriptionId { get; set; }

        [Required, StringLength(400)]
        public string Description { get; set; }

        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
