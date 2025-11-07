using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class DocumentViewModel
    {

     
        public string DocumentNode { get; set; }


        public string Title { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public decimal Revision { get; set; }
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<ProductDocumentViewModel> ProductDocuments { get; set; }
        public DocumentViewModel()
        {
            ProductDocuments = new HashSet<ProductDocumentViewModel>();
        }
    }
}
