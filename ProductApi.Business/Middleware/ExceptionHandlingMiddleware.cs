using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;

namespace ProductApi.Business.Middleware
{
    public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

        /// <summary>
		/// İsteği işlerken oluşabilecek hataları yakalar ve uygun yanıtı döndürür.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (UnauthorizedAccessException ex)
			{
				Log.Warning(ex, "Yetkisiz erişim: {Message}", ex.Message);
				await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, "Geçersiz token veya kimlik bilgileri.");
			}
			catch (KeyNotFoundException ex)
			{
				Log.Warning(ex, "Kaynak bulunamadı: {Message}", ex.Message);
				await HandleExceptionAsync(context, HttpStatusCode.NotFound, "Aradığınız kaynak bulunamadı.");
			}
			catch (ArgumentException ex)
			{
				Log.Warning(ex, "Geçersiz argüman: {Message}", ex.Message);
				await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Geçersiz argüman.");
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Bir hata oluştu: {Message}", ex.Message);
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
			}
		}

        /// <summary>
		/// Yakalanan hatalar için HTTP yanıtını oluşturur.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="statusCode"></param>
		/// <param name="message"></param>
		/// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
		{
			context.Response.StatusCode = (int)statusCode;
			context.Response.ContentType = "application/json";

			var response = JsonSerializer.Serialize(new { message });
			return context.Response.WriteAsync(response);
		}
	}
}
