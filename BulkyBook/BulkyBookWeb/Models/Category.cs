using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        public Category(int id, string? name, int displayOrder)
        {
            Id = id;
            Name = name;
            DisplayOrder = displayOrder;
            dataRegistro = DateTime.Now;
        }

        [Key]
        public int Id { get; private set; }
        [Required]
        public string? Name { get; private set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display order must be between 1 and 100 only")]
        public int DisplayOrder { get; private set; }

        public DateTime dataRegistro { get; private set; }



    }
}
