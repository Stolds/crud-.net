using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class CategoryViewModel
    {

        
        public int Id { get; set; }
        
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100 only")]
        public int DisplayOrder { get; set; }

    }
}
