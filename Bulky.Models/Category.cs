using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        // How can we say that a property is the primary key
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Display Error should be from range 1-100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

    }
}
