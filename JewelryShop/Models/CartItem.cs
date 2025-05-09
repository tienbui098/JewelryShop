using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        public decimal Price { get; set; } // Giá tại thời điểm thêm vào giỏ hàng

        // Foreign keys
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Tính tổng tiền cho mỗi item
        public decimal Subtotal
        {
            get { return Quantity * Price; }
        }
    }
} 