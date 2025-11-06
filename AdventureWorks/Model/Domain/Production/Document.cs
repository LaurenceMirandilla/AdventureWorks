using Microsoft.SqlServer.Types;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Model.Domain.Production
{
    public class Document
    {
        public Document()
        {
            ProductDocuments = new HashSet<ProductDocument>();
        }
        [Key]
        public string DocumentNode { get; set; }
        public string Title { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public decimal Revision { get; set; }
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductDocument> ProductDocuments { get; set; }
    }
}
