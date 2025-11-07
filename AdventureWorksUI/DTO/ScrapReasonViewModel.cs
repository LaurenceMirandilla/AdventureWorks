using System.ComponentModel.DataAnnotations;

namespace AdventureWorksUI.DTO
{
    public class ScrapReasonViewModel
    {
        public short ScrapReasonId { get; set; }
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
