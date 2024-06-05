using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartCart.DataProvider.DatabaseContext;
using SmartCart.DataProvider.Models;

namespace SmartCart.DataProvider.Repositories
{
    public class CartMemberRepository : ICartMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartMemberRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(CartMemberPost cartMemberPost)
        {
            if (cartMemberPost == null || string.IsNullOrEmpty(cartMemberPost.UserMail))
            {
                return false;
            }

            string userId = string.Empty;

            var commandText = "SELECT TOP 1 UserID FROM [dbo].[Users] WHERE Email = @Email";
            var emailParameter = new SqlParameter("@Email", cartMemberPost.UserMail);

            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.Parameters.Add(emailParameter);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        userId = reader["UserID"].ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            var userIdGuid = new Guid(userId);

            var cartMember = new CartMember
            {
                CartID = cartMemberPost.CartID,
                MemberID = userIdGuid
            };

            await _context.CartMembers.AddAsync(cartMember);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid cartMemberId)
        {
            var cartMember = _context.CartMembers.Find(cartMemberId);
            
            _context.CartMembers.Remove(cartMember);
            var result = await _context.SaveChangesAsync();
            
            return result > 0;
        }

        public async Task<bool> DeleteAsyncByMemberAndCart(Guid memberId, Guid cartID)
        {
            var cartMember = await _context.CartMembers.Where(c => c.CartMemberID == memberId && c.CartID == cartID).ToListAsync();

            _context.CartMembers.RemoveRange(cartMember);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<CartMemberDto>> Retrieve()
        {
            var cartMembers = await _context.CartMembers.ToListAsync();
            return _mapper.Map<List<CartMemberDto>>(cartMembers);
        }

        public async Task<List<CartMemberDto>> RetrieveByCartIDAsync(Guid cartId)
        {
            var cartMembers = _context.CartMembers.Where(x => x.CartID == cartId).ToList();
            return _mapper.Map<List<CartMemberDto>>(cartMembers);
        }

        public async Task<List<CartMemberDto>> RetrieveByIdAsync(Guid cartMemberId)
        {
            var cartMembers = await _context.CartMembers.Where(x => x.CartMemberID == cartMemberId).ToListAsync();
            return _mapper.Map<List<CartMemberDto>>(cartMembers);
        }

        public async Task<List<CartMemberDto>> RetrieveByUserIDAsync(Guid memberId)
        {
            var cartMembers = await _context.CartMembers.Where(x => x.MemberID == memberId).ToListAsync();
            return _mapper.Map<List<CartMemberDto>>(cartMembers);
        }

        public async Task<bool> UpdateAsync(CartMemberDto cartMember)
        {
            if (cartMember == null)
            {
                return false;
            }

            var cartMemberEntity = _mapper.Map<CartMember>(cartMember);

            _context.CartMembers.Update(cartMemberEntity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(List<CartMemberDto> cartMemberDtos)
        {
            if (cartMemberDtos == null)
            {
                return false;
            }

            var cartMemberEntities = _mapper.Map<List<CartMember>>(cartMemberDtos);

            _context.CartMembers.UpdateRange(cartMemberEntities);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
