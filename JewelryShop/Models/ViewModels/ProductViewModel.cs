using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Brand { get; set; }
        
        // Thông tin SEO
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }

        // Danh sách hình ảnh
        public List<ProductImageViewModel> Images { get; set; }
    }

    public class ProductImageViewModel
    {
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsMainImage { get; set; }
        public bool Is360Image { get; set; }
        public int DisplayOrder { get; set; }
    }
} 