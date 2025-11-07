using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductModelIllustrationViewModel
    {
        public int ProductModelIllustrationId { get; set; }
        public int ProductModelId { get; set; }

        public int IllustrationId { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
