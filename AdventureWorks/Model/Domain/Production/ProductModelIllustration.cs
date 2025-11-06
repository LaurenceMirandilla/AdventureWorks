namespace AdventureWorks.Model.Domain.Production


{
    public  class ProductModelIllustration
    {
        public int ProductModelIllustrationId { get; set; }
        public int ProductModelId { get; set; }
        public int IllustrationId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Illustration Illustration { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}

