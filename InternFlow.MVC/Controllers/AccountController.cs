using InternFlow.BLL.Interfaces;
using InternFlow.BLL.Services;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternFlow.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AccountController(IAuthService authService, IConfiguration configuration, IUserService userService)
        {
            _authService = authService;
            _configuration = configuration;
            _userService = userService;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _authService.Login(username, password);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                return View();
            }

            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.Username ?? ""),
                 new Claim(ClaimTypes.Role, user.Role ?? "Stajyer"),
                 new Claim(ClaimTypes.Email, user.Email ?? "")
             };

            var claimsIdentity = new ClaimsIdentity(claims, "MvcCookie");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("MvcCookie", claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, string password)
        {
            try
            {
                _authService.Register(user, password);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MvcCookie");
            return RedirectToAction("Login");
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? ""),
                new Claim(ClaimTypes.Role, user.Role ?? "Stajyer"),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //HEsab ayarlar guncelleme .
        [Authorize]
        public IActionResult Profile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = _userService.GetById(userId);
            return View(user);
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult Profile(User user, string? newPassword)
        {
            var existing = _userService.GetById(user.Id);

            existing.FullName = user.FullName;
            existing.Email = user.Email;
            existing.Username = user.Username;

            if (!string.IsNullOrEmpty(newPassword))
            {
                existing.Password = _authService.HashPassword(newPassword);
            }

            _userService.Update(existing);

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }
    }
}