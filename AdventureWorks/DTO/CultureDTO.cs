

using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.DTO
{
    public class CultureDTO
    {
        [Required, StringLength(6)]
        public string CultureId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
