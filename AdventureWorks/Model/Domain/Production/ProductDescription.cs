namespace AdventureWorks.Model.Domain.Production
{
    public partial class ProductDescription
    {
        public int ProductDescriptionId { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
