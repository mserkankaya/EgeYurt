using ProductApi.Business.Interfaces;
using ProductApi.DataAccess.Repositories;
using ProductApi.Entity.Concrete;

namespace ProductApi.Business.Services
{
    public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

        /// <summary>
        /// Tüm ürünleri asenkron olarak almak için kullanılır.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _productRepository.GetAllAsync();

        /// <summary>
        /// Belirli bir ürünün ID'sine göre asenkron olarak alınmasını sağlar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(int id) => await _productRepository.GetByIdAsync(id);

        /// <summary>
        /// Yeni bir ürünü asenkron olarak ekler.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddProductAsync(Product product) => await _productRepository.AddAsync(product);

        /// <summary>
        /// Var olan bir ürünü asenkron olarak günceller.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateProductAsync(Product product) => await _productRepository.UpdateAsync(product);

        /// <summary>
        /// Belirli bir ürünü asenkron olarak siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProductAsync(int id) => await _productRepository.DeleteAsync(id);
	}
}
//Asenkron, bir işlemin tamamlanmasını beklemeden diğer işlemlerin devam etmesine olanak tanıyan bir programlama modelidir.