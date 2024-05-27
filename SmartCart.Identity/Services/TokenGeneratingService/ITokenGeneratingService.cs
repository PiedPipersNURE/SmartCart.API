using Microsoft.IdentityModel.Tokens;
using SmartCart.Identity.Models;
using System.Security.Claims;

namespace SmartCart.Identity.Services
{
    public interface ITokenGeneratingService
    {
        string GenerateToken(UserDto user);
        string GenerateToken(List<Claim> userClaims);
    }
}
