using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.Repositories
{
    public interface ICartMemberRepository
    {
        Task<List<CartMemberDto>> Retrieve();
        Task<List<CartMemberDto>> RetrieveByIdAsync(Guid cartMemberId);
        Task<List<CartMemberDto>> RetrieveByCartIDAsync(Guid cartId);
        Task<List<CartMemberDto>> RetrieveByUserIDAsync(Guid cartMemberId);
        Task<bool> AddAsync(CartMemberPost cartMemberPost);
        Task<bool> UpdateAsync(CartMemberDto cartMember);
        Task<bool> UpdateAsync(List<CartMemberDto> cartMemberDtos);
        Task<bool> DeleteAsync(Guid cartMemberId);
        Task<bool> DeleteAsyncByMemberAndCart(Guid memberId, Guid cartID);
    }
}
