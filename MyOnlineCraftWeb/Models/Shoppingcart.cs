using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyOnlineCraftWeb.Models.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineCraftWeb.Models
{
    public class Shoppingcart
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please enter between 1 and 1000")]
        public int count { get; set; }

        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        [ValidateNever]
        public AppUser AppUser { get; set; }



    }
}
