using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCart.DataProvider.Models;
using SmartCart.DataProvider.Repositories;

namespace SmartCart.DataProvider.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<List<CartDto>> Retrieve()
        {
            var carts = await _cartRepository.Retrieve();
            return carts;
        }

        [Authorize]
        [HttpGet("getById/{cartId}")]
        public async Task<CartDto> RetrieveByIdAsync(Guid cartId)
        {
            var cart = await _cartRepository.RetrieveByIdAsync(cartId);
            return cart;
        }

        [Authorize]
        [HttpPost("post")]
        public async Task<bool> AddAsync([FromBody] CartDto cart)
        {
            var result = await _cartRepository.AddAsync(cart);
            return result;
        }

        [Authorize]
        [HttpPost("postMultiple")]
        public async Task<bool> AddAsync([FromBody] List<CartDto> carts)
        {
            var result = await _cartRepository.AddAsync(carts);
            return result;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<bool> UpdateAsync([FromBody] CartDto cart)
        {
            var result = await _cartRepository.UpdateAsync(cart);
            return result;
        }

        [Authorize]
        [HttpPut("updateMultiple")]
        public async Task<bool> UpdateAsync([FromBody] List<CartDto> carts)
        {
            var result = await _cartRepository.UpdateAsync(carts);
            return result;
        }

        [Authorize]
        [HttpDelete("delete/{cartId}")]
        public async Task<bool> DeleteAsync(Guid cartId)
        {
            var result = await _cartRepository.DeleteAsync(cartId);
            return result;
        }
    }
}
