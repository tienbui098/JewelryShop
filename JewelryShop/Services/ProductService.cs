using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JewelryShop.Models;
using JewelryShop.Models.ViewModels;
using JewelryShop.Repositories;
using JewelryShop.Data;

namespace JewelryShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductImage> _imageRepository;
        private readonly ApplicationDbContext _context;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<ProductImage> imageRepository,
            ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _context = context;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return null;

            return MapToViewModel(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .ToListAsync();

            return products.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProductViewModel>> GetFeaturedProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Where(p => p.IsFeatured && p.IsActive)
                .ToListAsync();

            return products.Select(MapToViewModel);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();

            return products.Select(MapToViewModel);
        }

        public async Task<ProductViewModel> CreateProductAsync(ProductViewModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                SKU = viewModel.SKU,
                Description = viewModel.Description,
                Price = viewModel.Price,
                DiscountPrice = viewModel.DiscountPrice,
                StockQuantity = viewModel.StockQuantity,
                Material = viewModel.Material,
                Size = viewModel.Size,
                Weight = viewModel.Weight,
                Brand = viewModel.Brand,
                MetaTitle = viewModel.MetaTitle,
                MetaDescription = viewModel.MetaDescription,
                Slug = viewModel.Slug,
                CategoryId = viewModel.CategoryId,
                IsActive = true,
                IsFeatured = viewModel.IsFeatured
            };

            await _productRepository.AddAsync(product);
            await _context.SaveChangesAsync();

            return MapToViewModel(product);
        }

        public async Task UpdateProductAsync(ProductViewModel viewModel)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ProductId == viewModel.ProductId);

            if (product == null) throw new Exception("Product not found");

            product.Name = viewModel.Name;
            product.SKU = viewModel.SKU;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.DiscountPrice = viewModel.DiscountPrice;
            product.StockQuantity = viewModel.StockQuantity;
            product.Material = viewModel.Material;
            product.Size = viewModel.Size;
            product.Weight = viewModel.Weight;
            product.Brand = viewModel.Brand;
            product.MetaTitle = viewModel.MetaTitle;
            product.MetaDescription = viewModel.MetaDescription;
            product.Slug = viewModel.Slug;
            product.CategoryId = viewModel.CategoryId;
            product.IsActive = viewModel.IsActive;
            product.IsFeatured = viewModel.IsFeatured;
            product.UpdatedAt = DateTime.Now;

            _productRepository.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            _productRepository.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddProductImageAsync(int productId, ProductImage image)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return false;

            image.ProductId = productId;
            await _imageRepository.AddAsync(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProductImageAsync(int productId, int imageId)
        {
            var image = await _imageRepository.GetByIdAsync(imageId);
            if (image == null || image.ProductId != productId) return false;

            _imageRepository.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductImageOrderAsync(int productId, List<int> imageIds)
        {
            var images = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            if (!images.Any()) return false;

            for (int i = 0; i < imageIds.Count; i++)
            {
                var image = images.FirstOrDefault(pi => pi.ProductImageId == imageIds[i]);
                if (image != null)
                {
                    image.DisplayOrder = i;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetMainImageAsync(int productId, int imageId)
        {
            var images = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            var targetImage = images.FirstOrDefault(pi => pi.ProductImageId == imageId);
            if (targetImage == null) return false;

            foreach (var image in images)
            {
                image.IsMainImage = image.ProductImageId == imageId;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Set360ImageAsync(int productId, int imageId)
        {
            var images = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            var targetImage = images.FirstOrDefault(pi => pi.ProductImageId == imageId);
            if (targetImage == null) return false;

            foreach (var image in images)
            {
                image.Is360Image = image.ProductImageId == imageId;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private ProductViewModel MapToViewModel(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                StockQuantity = product.StockQuantity,
                Material = product.Material,
                Size = product.Size,
                Weight = product.Weight,
                Brand = product.Brand,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                Slug = product.Slug,
                IsActive = product.IsActive,
                IsFeatured = product.IsFeatured,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                AverageRating = product.Reviews?.Any() == true 
                    ? product.Reviews.Average(r => r.Rating) 
                    : 0,
                ReviewCount = product.Reviews?.Count ?? 0,
                Images = product.Images?.Select(i => new ProductImageViewModel
                {
                    ProductImageId = i.ProductImageId,
                    ImageUrl = i.ImageUrl,
                    AltText = i.AltText,
                    IsMainImage = i.IsMainImage,
                    Is360Image = i.Is360Image,
                    DisplayOrder = i.DisplayOrder
                }).ToList()
            };
        }
    }
} 