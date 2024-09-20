namespace Consume.Services
{
	public class AuthService
	{
        /// <summary>
        /// kullanıcı kimlik doğrulama işlemlerini gerçekleştiren bir hizmettir.
        /// </summary>
		/// 
        private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

        /// <summary>
        ///   /// HttpClient: Uygulamanın dış API'lerle iletişim kurmasını sağlar.
        /// Constructor: HttpClient nesnesini alarak _httpClient değişkenine atar
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> LoginAsync(string username, string password)
		{
			var loginDto = new { username, password };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7268/api/Auth/login", loginDto);
            response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<ResponseDto>();
			return result.Token;
		}

        /// <summary>
        ///  API yanıtından dönen token'ı tutmak için kullanılır.
        /// </summary>
        private class ResponseDto
		{
			public string Token { get; set; }
		}
	}
}
