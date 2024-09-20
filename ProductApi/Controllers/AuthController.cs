using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Business.Interfaces;
using ProductApi.Dto.Login;
using ProductApi.Entity.Concrete;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductApi.Controllers
{

    /// <summary>
	/// AuthController, kullanıcı kimlik doğrulama ve çıkış işlemlerini yönetir. JWT kullanarak güvenli bir oturum sağlar ve token'ların geçerliliğini yönetir. Kullanıcı giriş denemeleri ve çıkış işlemleri sırasında loglama yapılır.
	/// </summary>

    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		//Constructor
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public AuthController(IUserService userService, IConfiguration configuration, ITokenBlacklistService tokenBlacklistService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenBlacklistService = tokenBlacklistService;
        }

        /// <summary>
		/// Kullanıcının giriş yapmasını sağlar.
		/// </summary>
		/// <param name="loginDto">Kullanıcı adı ve şifre içeren DTO.</param>
		/// <returns></returns>
        [HttpPost("login")]
		public IActionResult Login([FromBody] LoginDto loginDto)
		{
			Log.Information("Kullanıcı girişi denemesi: {Username}", loginDto.Username);

			var user = _userService.Authenticate(loginDto.Username, loginDto.Password);

			if (user == null)
			{
				Log.Warning("Giriş başarısız: {Username}", loginDto.Username);
				return BadRequest(); 
			}
			var token = GenerateJwtToken(user);
			Log.Information("Kullanıcı başarıyla giriş yaptı: {Username},Token: {Token}", user.Username);
			return Ok(new { Token = token });
		}

        /// <summary>
		///  Verilen kullanıcı bilgilerine göre bir JWT token oluşturur.
		/// </summary>
		/// <param name="user">Kullanıcı</param>
		/// <returns></returns>
        private string GenerateJwtToken(User user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); 
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"], 
				audience: _configuration["Jwt:Issuer"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

        /// <summary>
		/// Kullanıcının çıkış yapmasını sağlar.
		/// </summary>
		/// <returns></returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Token'ı kara listeye ekler.
            _tokenBlacklistService.BlacklistToken(token);

            Log.Information("Kullanıcı çıkış yaptı: {Username}", User.Identity.Name);
            return Ok(new { Message = "Çıkış başarılı" });
        }
    }
}
