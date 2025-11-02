using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class DocumentDTO
    {
        [Required]
        public string DocumentNode { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string? FileName { get; set; }

        [StringLength(20)]
        public string? FileExtension { get; set; }

        [Range(0, 99999999)]
        public int? ChangeNumber { get; set; }

        public byte Status { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
