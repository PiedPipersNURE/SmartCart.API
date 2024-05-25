using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCart.DataProvider.Models;
using SmartCart.DataProvider.Repositories;

namespace SmartCart.DataProvider.Controllers
{
    [Route("api/[controller]")]
    public class CartMemberController : ControllerBase
    {
        private readonly ICartMemberRepository _cartMemberRepository;

        public CartMemberController(ICartMemberRepository cartMemberRepository)
        {
            _cartMemberRepository = cartMemberRepository;
        }

        [Authorize]
        [HttpGet("retrieve")]
        public async Task<List<CartMemberDto>> Retrieve()
        {
            var cartMembers = await _cartMemberRepository.Retrieve();
            return cartMembers;
        }

        [Authorize]
        [HttpGet("getById/{cartMemberId}")]
        public async Task<List<CartMemberDto>> RetrieveByIdAsync(Guid cartMemberId)
        {
            var cartMembers = await _cartMemberRepository.RetrieveByIdAsync(cartMemberId);
            return cartMembers;
        }

        [Authorize]
        [HttpGet("getByCartId/{cartId}")]
        public async Task<List<CartMemberDto>> RetrieveByCartIDAsync(Guid cartId)
        {
            var cartMembers = await _cartMemberRepository.RetrieveByCartIDAsync(cartId);
            return cartMembers;
        }

        [Authorize]
        [HttpGet("getByUserId/{userId}")]
        public async Task<List<CartMemberDto>> RetrieveByUserIDAsync(Guid userId)
        {
            var cartMembers = await _cartMemberRepository.RetrieveByUserIDAsync(userId);
            return cartMembers;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<bool> AddAsync([FromBody] CartMemberDto cartMember)
        {
            var result = await _cartMemberRepository.AddAsync(cartMember);
            return result;
        }

        [Authorize]
        [HttpPost("addMultiple")]
        public async Task<bool> AddAsync([FromBody] List<CartMemberDto> cartMembers)
        {
            var result = await _cartMemberRepository.AddAsync(cartMembers);
            return result;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<bool> UpdateAsync([FromBody] CartMemberDto cartMember)
        {
            var result = await _cartMemberRepository.UpdateAsync(cartMember);
            return result;
        }

        [Authorize]
        [HttpPut("updateMultiple")]
        public async Task<bool> UpdateAsync([FromBody] List<CartMemberDto> cartMemberDtos)
        {
            var result = await _cartMemberRepository.UpdateAsync(cartMemberDtos);
            return result;
        }

        [Authorize]
        [HttpDelete("delete/{cartMemberId}")]
        public async Task<bool> DeleteAsync(Guid cartMemberId)
        {
            var result = await _cartMemberRepository.DeleteAsync(cartMemberId);
            return result;
        }

        [Authorize]
        [HttpDelete("deleteByMemberAndCart/{memberId}/{cartID}")]
        public async Task<bool> DeleteAsyncByMemberAndCart(Guid memberId, Guid cartID)
        {
            var result = await _cartMemberRepository.DeleteAsyncByMemberAndCart(memberId, cartID);
            return result;
        }
    }
}
