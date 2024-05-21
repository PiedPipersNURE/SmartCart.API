using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartDto>> Retrieve();
        Task<CartDto> RetrieveByIdAsync(Guid cartId);
        Task<bool> AddAsync(CartDto cart);
        Task<bool> AddAsync(List<CartDto> carts);
        Task<bool> UpdateAsync(CartDto carts);
        Task<bool> UpdateAsync(List<CartDto> carts);
        Task<bool> DeleteAsync(Guid cartID);
    }
}
