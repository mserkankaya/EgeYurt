using Consume.Models;
using System.Net.Http.Headers;

namespace Consume.Services
{
    /// <summary>
    ///  ürünlerle ilgili işlemleri gerçekleştiren hizmettir. 
    ///  HttpClient: Dış API'lerle iletişim kurmak için kullanılır.
    ///  ProductService(HttpClient httpClient): HttpClient nesnesini alarak _httpClient değişkenine atar.
    /// </summary>
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/api/Product");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {content}");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }

        /// <summary>
        /// Yeni ürün ekleme
        /// </summary>
        /// <param name="product">Product nesnesi</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddProductAsync(Product product, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("/api/product", product);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {content}");
            }
        }

        /// <summary>
        /// Ürün güncelleme
        /// </summary>
        /// <param name="product"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateProductAsync(Product product, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync($"/api/product/{product.Id}", product);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {content}");
            }
        }

        /// <summary>
        /// Ürün silme
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteProductAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"/api/product/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {content}");
            }
        }
    }
}
