namespace AdventureWorks.Model.Domain.Production


{
    public  class ProductPhoto
    {
        public ProductPhoto()
        {
            ProductProductPhotos = new HashSet<ProductProductPhoto>();
        }

        public int ProductPhotoId { get; set; }
        public string? ThumbNailPhoto { get; set; }
        public string? ThumbnailPhotoFileName { get; set; }
        public string? LargePhoto { get; set; }
        public string? LargePhotoFileName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductProductPhoto> ProductProductPhotos { get; set; }
    }
}
//???????????????