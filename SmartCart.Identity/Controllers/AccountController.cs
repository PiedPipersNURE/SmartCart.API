using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using SmartCart.Identity.Models;
using SmartCart.Identity.Repository;
using SmartCart.Identity.Services;
using System.Security.Claims;

[Route("account")]
public class AccountController : Controller
{
    private readonly ITokenGeneratingService _tokenGeneratingService;
    private readonly IUserRepository _userRepository;
    
    public AccountController(ITokenGeneratingService tokenGeneratingService, IUserRepository userRepository)
    {
        _tokenGeneratingService = tokenGeneratingService;
        _userRepository = userRepository;
    }

    [HttpGet("google-login")]
    public IActionResult GoogleLogin(string returnUrl = "/")
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse", new { returnUrl }) };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-response")]
    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
        {
            return BadRequest();
        }

        var claims = authenticateResult.Principal.Claims.ToList();

        var googleId = claims.Where(x => x.Type.ToString() == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

        var user = await _userRepository.Get(googleId);
        string? jwtToken;

        if (user == null)
        {
            var registrationModel = new RegistrationModel
            {
                GoogleID = googleId,
                Email = claims.Where(x => x.Type.ToString() == ClaimTypes.Email).FirstOrDefault().Value,
                Username = claims.Where(x => x.Type.ToString() == ClaimTypes.Name).FirstOrDefault().Value,
            };

            user = await _userRepository.Insert(registrationModel, IsGoogleRegistration: true);
        }

        jwtToken = _tokenGeneratingService.GenerateToken(user);

        return Ok(jwtToken);
    }

    [HttpGet("login/{email}/{password}")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var loginModel = new LoginModel
        {
            Email = email,
            Password = password
        };

        var user = await _userRepository.Login(loginModel);
        if(user == null)
        {
            return NotFound();
        }

        var jwtToken = _tokenGeneratingService.GenerateToken(user);
        return Ok(jwtToken);
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Registration([FromBody]RegistrationModel registrationModel)
    {
        var result = await _userRepository.Insert(registrationModel);
        if(result != null)
        {
            var jwtToken = _tokenGeneratingService.GenerateToken(result);
            return Ok(jwtToken);
        }

        return BadRequest();
    }

    [HttpGet("google-mobile-login/{googleId}/{username}/{email}")]
    public async Task<IActionResult> GoogleMobileLogin(string googleId, string username, string email)
    {
        if (string.IsNullOrEmpty(googleId) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var result = await _userRepository.Get(googleId);
        if (result != null)
        {
            var jwtToken = _tokenGeneratingService.GenerateToken(result);
            return Ok(jwtToken);
        }
        else
        {
            var registrationModel = new RegistrationModel
            {
                GoogleID = googleId,
                Email = email,
                Username = username,
            };

            var user = await _userRepository.Insert(registrationModel, IsGoogleRegistration: true);
            if (user != null)
            {
                var jwtToken = _tokenGeneratingService.GenerateToken(user);
                return Ok(jwtToken);
            }
        }

        return NotFound();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser(UserDto userDto)
    {
        var result = await _userRepository.Update(userDto);

        return result ? Ok() : BadRequest();
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
