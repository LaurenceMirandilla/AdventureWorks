using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class ProductPhotoDTO
    {
        public int ProductPhotoId { get; set; }

        public byte[]? ThumbNailPhoto { get; set; }

        [StringLength(50)]
        public string? ThumbnailPhotoFileName { get; set; }

        public byte[]? LargePhoto { get; set; }

        [StringLength(50)]
        public string? LargePhotoFileName { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
