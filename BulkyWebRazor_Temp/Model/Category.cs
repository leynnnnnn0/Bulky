using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyBookWebRazor_Temp.Model
{
    public class Category
    {
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
