using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyOnlineCraftWeb.Models.ViewModel
{
    public class AppUser: IdentityUser 
    {
        [Required]
        public string name { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postalCode { get; set; }
    }
}
