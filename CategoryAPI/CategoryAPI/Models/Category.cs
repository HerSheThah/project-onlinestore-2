using System;
using System.Collections.Generic;

namespace CategoryAPI.Models
{
    public partial class Category
    {
        public int CatId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
