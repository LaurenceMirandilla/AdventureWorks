namespace AdventureWorks.Model.Domain.Production
{
    public partial class ProductDocument
    {
        public int ProductId { get; set; }
        public string DocumentNode { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Document DocumentNodeNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
