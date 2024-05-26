using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartCart.Identity;
using SmartCart.Identity.Models;
using SmartCart.Identity.Repository;
using SmartCart.Identity.Services;

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
    public IActionResult Login(string returnUrl = "/")
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

        var accessToken = authenticateResult.Properties.GetTokenValue("access_token");

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{SD.GoogleAPIEndpointURL}{accessToken}");

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest();
        }

        var userInfoJson = await response.Content.ReadAsStringAsync();
        var userInfo = JsonConvert.DeserializeObject<dynamic>(userInfoJson);

        var user = await _userRepository.Get(userInfo.GoogleId.ToString());
        string? jwtToken;

        if (user != null)
        {
            jwtToken = _tokenGeneratingService.GenerateToken(userInfo);
        }
        else
        {
            jwtToken = _tokenGeneratingService.GenerateToken(user);
        }

        return Ok(jwtToken);
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var user = await _userRepository.Login(loginModel);
        if(user == null)
        {
            return NotFound();
        }

        var jwtToken = _tokenGeneratingService.GenerateToken(user);
        return Ok(jwtToken);
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Registration(RegistrationModel registrationModel)
    {
        var result = await _userRepository.Insert(registrationModel);
        if(result != null)
        {
            var jwtToken = _tokenGeneratingService.GenerateToken(result);
            return Ok(jwtToken);
        }

        return BadRequest();
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
