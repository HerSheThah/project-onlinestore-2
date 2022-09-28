using System;
using System.Collections.Generic;

namespace ProductAPI.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public int DiscountPercent { get; set; }
        public double ActualPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int ProductCategoryId { get; set; }
    }
}
