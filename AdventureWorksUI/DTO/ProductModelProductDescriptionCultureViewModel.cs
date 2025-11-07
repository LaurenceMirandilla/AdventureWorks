using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductModelProductDescriptionCultureViewModel
    {
        public int ProductModelId { get; set; }

        public int ProductDescriptionId { get; set; }
        public string CultureId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
