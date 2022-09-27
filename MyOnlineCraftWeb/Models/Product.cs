using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineCraftWeb.Models
{
    public class Product
    {
        [Key]
        [DisplayName("ID")]

        public int productId { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string productName { get; set; }

        [DisplayName("Description")]

        public string productDescription { get; set; }

       
        public int discountPercent { get; set; }
        public Double ActualPrice { get; set; }
        public Double DiscountPrice { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string imageURL { get; set; }


        [DisplayName("Category")]
        public int productCategoryId { get; set; }
        [ForeignKey("productCategoryId")]
        [ValidateNever]
        public virtual Category Category { get; set; }

    }
}
