using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Navigation property
        public virtual ICollection<CartItem> CartItems { get; set; }

        // Tính tổng tiền giỏ hàng
        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                if (CartItems != null)
                {
                    foreach (var item in CartItems)
                    {
                        total += item.Subtotal;
                    }
                }
                return total;
            }
        }
    }
} 