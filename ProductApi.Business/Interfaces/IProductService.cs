using ProductApi.Entity.Concrete;

namespace ProductApi.Business.Interfaces
{
    /// <summary>
    /// ürünlerle ilgili iş mantığını tanımlayan bir yapıdır. Product nesneleri üzerinde temel CRUD işlemlerini tanımlar. 
    /// </summary>
    public interface IProductService
	{
        /// <summary>
        /// Tüm ürünlerin listesini asenkron olarak alır. Bu, veritabanındaki tüm ürünleri döndürür.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü asenkron olarak alır.
        /// </summary>
        /// <param name="id">ürün ID'si</param>
        /// <returns></returns>
		Task<Product> GetProductByIdAsync(int id);

        /// <summary>
        /// Yeni bir ürünü veritabanına asenkron olarak ekler.
        /// </summary>
        /// <param name="product">Product </param>
        /// <returns></returns>
		Task AddProductAsync(Product product);

        /// <summary>
        /// Var olan bir ürünü veritabanında asenkron olarak günceller.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
		Task UpdateProductAsync(Product product);

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü veritabanından asenkron olarak siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		Task DeleteProductAsync(int id);
	}
}
