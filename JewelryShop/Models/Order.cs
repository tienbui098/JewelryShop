using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Chờ xử lý, Đang xử lý, Đang giao hàng, Hoàn thành, Đã hủy

        [Required]
        [StringLength(200)]
        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        // Thông tin thanh toán
        [StringLength(50)]
        public string PaymentMethod { get; set; } // COD, Credit Card, Bank Transfer
        public string PaymentStatus { get; set; } // Chờ thanh toán, Đã thanh toán, Thất bại
        public string TransactionId { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Navigation property
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
} 