using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCart.DataProvider.DatabaseContext;
using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CartDto cart)
        {
            if(cart == null)
            {
                return false;
            }

            var cartEntity = _mapper.Map<Cart>(cart);

            await _context.Carts.AddAsync(cartEntity);
            await _context.CartMembers.AddAsync(new CartMember
            {
                CartID = cartEntity.CartID,
                MemberID = cartEntity.CartOwnerID
            });

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddAsync(List<CartDto> carts)
        {
            if (carts.Count == 0)
            {
                return false;
            }

            var cartEntities = _mapper.Map<List<Cart>>(carts);

            await _context.Carts.AddRangeAsync(cartEntities);
            var cartMembers = new List<CartMember>();
            cartEntities.ForEach(cart =>
            {
                cartMembers.Add(new CartMember
                {
                    CartID = cart.CartID,
                    MemberID = cart.CartOwnerID
                });
            });

            await _context.CartMembers.AddRangeAsync(cartMembers);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid cartID)
        {
            var cart = await _context.Carts.FindAsync(cartID);

            _context.Carts.Remove(cart);
            var result = await _context.SaveChangesAsync();
         
            return result > 0;
        }

        public async Task<List<CartDto>> Retrieve()
        {
            var carts = await _context.Carts.ToListAsync();
            return _mapper.Map<List<CartDto>>(carts);
        }

        public async Task<CartDto> RetrieveByIdAsync(Guid cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> UpdateAsync(CartDto cart)
        {
            if (cart == null)
            {
                return false;
            }

            var cartEntity = _mapper.Map<Cart>(cart);
            
            _context.Carts.Update(cartEntity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(List<CartDto> carts)
        {
            if (carts.Count == 0)
            {
                return false;
            }

            var cartEntities = _mapper.Map<List<Cart>>(carts);

            _context.Carts.UpdateRange(cartEntities);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
