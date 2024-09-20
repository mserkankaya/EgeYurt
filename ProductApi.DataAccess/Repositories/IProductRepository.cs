using ProductApi.Entity.Concrete;

namespace ProductApi.DataAccess.Repositories
{
    public interface IProductRepository
	{

        /// <summary>
        /// Tüm ürünleri asenkron olarak almak için kullanılır.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllAsync();


        /// <summary>
        /// Belirli bir ürünün ID'sine göre asenkron olarak alınmasını sağlar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetByIdAsync(int id);


        /// <summary>
        /// Yeni bir ürünü asenkron olarak ekler.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task AddAsync(Product product);


        /// <summary>
        /// Var olan bir ürünü asenkron olarak günceller.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task UpdateAsync(Product product);


        /// <summary>
        /// Belirli bir ürünü asenkron olarak siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        //Asenkron, bir işlemin tamamlanmasını beklemeden diğer işlemlerin devam etmesine olanak tanıyan bir programlama modelidir.
    }
}
