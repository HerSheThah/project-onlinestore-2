using System;
using System.Collections.Generic;

namespace CartAPI.Models
{
    public partial class Shoppingcart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string AppUserId { get; set; } = null!;
    }
}
