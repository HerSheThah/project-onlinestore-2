using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineCraftWeb.Models
{
    public class Category
    {
        [Key]
        [DisplayName("ID")]
        public int catId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string categoryName { get; set; }
    }
}
