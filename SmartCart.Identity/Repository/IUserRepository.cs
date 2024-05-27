using SmartCart.Identity.Models;

namespace SmartCart.Identity.Repository
{
    public interface IUserRepository
    {
        Task<UserDto> Get(Guid userID);
        Task<UserDto> Get(string googleId);
        Task<UserDto> Login(LoginModel loginModel);
        Task<UserDto> Insert(RegistrationModel user, bool IsGoogleRegistration = false);
        Task<bool> Update(UserDto user);
    }
}
