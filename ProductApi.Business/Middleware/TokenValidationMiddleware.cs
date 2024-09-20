using Microsoft.AspNetCore.Http;
using ProductApi.Business.Interfaces;

namespace ProductApi.Business.Middleware
{
    /// <summary>
    /// gelen HTTP isteklerini kontrol ederek geçersiz tokenları yönetmek için bir middleware.
    /// Uygulamada güvenliği artırmak için kullanılır. Kullanıcıların geçersiz token'lar ile API'ye erişimini engeller
    /// </summary>
    public class TokenValidationMiddleware
    {
        /// <summary>
        /// Middleware zincirinde bir sonraki middleware'i çağırmak için kullanılır.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Middleware'i oluştururken bir sonraki middleware'i ve token kara liste hizmetini alır.
        /// </summary>
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public TokenValidationMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
        {
            _next = next;
            _tokenBlacklistService = tokenBlacklistService;
        }

        /// <summary>
        /// HTTP isteği geldiğinde çağrılan metot.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (_tokenBlacklistService.IsTokenBlacklisted(token))
            {
                context.Response.StatusCode = 401; // Yetkisizs
                await context.Response.WriteAsync("Token geçersiz.");
                return;
            }

            await _next(context);
        }
    }
}
