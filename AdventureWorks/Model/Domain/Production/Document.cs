namespace AdventureWorks.Model.Domain.Production
{
    public partial class Document
    {
        public Document()
        {
            ProductDocuments = new HashSet<ProductDocument>();
        }

        public string DocumentNode { get; set; }
        public byte[] DocumentSummary { get; set; }
        public string Title { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public decimal Revision { get; set; }
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductDocument> ProductDocuments { get; set; }
    }
}
