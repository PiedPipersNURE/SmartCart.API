using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> Retrieve();
        Task<ProductDto> RetrieveByIdAsync(Guid productId);
        Task<List<ProductDto>> RetrieveByNameAsync(string name);
        Task<List<ProductDto>> RetrieveByCartAndUserAsync(Guid cartID, Guid UserID);
        Task<List<ProductDto>> RetrieveByUserAsync(Guid UserID);
        Task<bool> AddAsync(ProductDto product);
        Task<bool> AddAsync(List<ProductDto> products);
        Task<bool> UpdateAsync(ProductDto product);
        Task<bool> UpdateAsync(List<ProductDto> products);
        Task<bool> DeleteAsync(Guid productId);
    }
}