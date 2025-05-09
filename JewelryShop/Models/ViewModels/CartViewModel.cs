using System.Collections.Generic;

namespace JewelryShop.Models.ViewModels
{
    public class CartViewModel
    {
        public int CartViewModelId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItems { get; set; }
    }

    public class CartItemViewModel
    {
        public int CartItemViewModelId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
} 