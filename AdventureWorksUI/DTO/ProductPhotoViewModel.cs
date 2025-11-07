using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ProductPhotoViewModel
    {
        public int ProductPhotoId { get; set; }

        public string? ThumbNailPhoto { get; set; }

        [StringLength(50)]
        public string? ThumbnailPhotoFileName { get; set; }

        public string? LargePhoto { get; set; }

        [StringLength(50)]
        public string? LargePhotoFileName { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
