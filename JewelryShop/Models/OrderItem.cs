using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; } // Giá tại thời điểm đặt hàng

        // Foreign keys
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Tính tổng tiền cho mỗi item
        public decimal Subtotal
        {
            get { return Quantity * Price; }
        }
    }
} 