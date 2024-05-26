using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCart.Identity.DatabaseContext;
using SmartCart.Identity.Models;
using SmartCart.Identity.Services;

namespace SmartCart.Identity.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Get(Guid userID)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userID);
            if(user != null)
            {
                return _mapper.Map<UserDto>(user);
            }

            return null;
        }

        public async Task<UserDto> Get(string googleId)
        {
            if (!string.IsNullOrEmpty(googleId))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleID == googleId);
                if (user != null)
                {
                    return _mapper.Map<UserDto>(user);
                }
            }

            return null;
        }

        public async Task<UserDto> Insert(RegistrationModel registrationModel)
        {
            if (registrationModel != null)
            {
                var userEntity = new User()
                {
                    Username = registrationModel.Username,
                    Email = registrationModel.Email,
                    FullName = registrationModel.Fullname,
                    BirthDate = registrationModel.Birthdate,
                };

                var hashedPassword = PasswordHasher.HashPassword(registrationModel.Password);

                userEntity.PasswordHash = hashedPassword.hash;
                userEntity.PasswordSalt = hashedPassword.salt;

                await _context.Users.AddAsync(userEntity);
                var result = await _context.SaveChangesAsync();
                return result > 0 ? _mapper.Map<UserDto>(userEntity) : null;
            }

            return null;
        }

        public async Task<UserDto> Login(LoginModel loginModel)
        {
            if(loginModel != null 
                && !string.IsNullOrEmpty(loginModel.Email) 
                && !string.IsNullOrEmpty(loginModel.Password))
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginModel.Email);
                if (user != null && PasswordHasher.VerifyPassword(loginModel.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return _mapper.Map<UserDto>(user);
                }
            }

            return null;
        }

        public async Task<bool> Update(UserDto user)
        {
            var userEntity = _mapper.Map<User>(user);
            if (userEntity != null)
            {
                _context.Users.Update(userEntity);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;   
        }
    }
}
