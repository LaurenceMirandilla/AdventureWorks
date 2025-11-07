using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductReviewDTO
    {
        public int ProductReviewId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, StringLength(50)]
        public string ReviewerName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int Rating { get; set; }

        [StringLength(3850)]
        public string? Comments { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
