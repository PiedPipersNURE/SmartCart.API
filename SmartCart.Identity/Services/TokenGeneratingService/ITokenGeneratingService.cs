using Microsoft.IdentityModel.Tokens;
using SmartCart.Identity.Models;

namespace SmartCart.Identity.Services
{
    public interface ITokenGeneratingService
    {
        string GenerateToken(UserDto user);
        string GenerateToken(dynamic user);
    }
}
