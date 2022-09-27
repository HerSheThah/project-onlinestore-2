using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineCraftWeb.Models.ViewModel
{
    public class ProductVM
    {
     
        public Product Product { get; set; }

        [ValidateNever]
        public IFormFile image { get; set; }

    }
}
