namespace MyOnlineCraftWeb.Models.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<Shoppingcart> shoppingcartList { get; set; }
        public double cartTotal { get; set; }
    }
}
