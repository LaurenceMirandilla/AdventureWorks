using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class IllustrationDTO
    {
        public int IllustrationId { get; set; }
        public string? Diagram { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
