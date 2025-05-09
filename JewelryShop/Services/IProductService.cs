using System.Collections.Generic;
using System.Threading.Tasks;
using JewelryShop.Models;
using JewelryShop.Models.ViewModels;

namespace JewelryShop.Services
{
    public interface IProductService
    {
        Task<ProductViewModel> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<IEnumerable<ProductViewModel>> GetFeaturedProductsAsync();
        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductViewModel> CreateProductAsync(ProductViewModel product);
        Task UpdateProductAsync(ProductViewModel product);
        Task DeleteProductAsync(int id);
        Task<bool> AddProductImageAsync(int productId, ProductImage image);
        Task<bool> RemoveProductImageAsync(int productId, int imageId);
        Task<bool> UpdateProductImageOrderAsync(int productId, List<int> imageIds);
        Task<bool> SetMainImageAsync(int productId, int imageId);
        Task<bool> Set360ImageAsync(int productId, int imageId);
    }
} 