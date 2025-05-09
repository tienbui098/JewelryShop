using System;
using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [StringLength(200)]
        public string AltText { get; set; } // Mô tả hình ảnh cho SEO

        public bool IsMainImage { get; set; } = false; // Hình ảnh chính
        public bool Is360Image { get; set; } = false; // Hình ảnh 360 độ
        public int DisplayOrder { get; set; } = 0; // Thứ tự hiển thị

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Foreign key
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
} 