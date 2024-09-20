using Microsoft.EntityFrameworkCore;
using ProductApi.DataAccess.Concrete;
using ProductApi.Entity.Concrete;

namespace ProductApi.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
	{
		private readonly EgeYurtContext _context;

		public ProductRepository(EgeYurtContext context)
		{
			_context = context;
		}

        /// <summary>
		/// Tüm ürünleri asenkron olarak almak için kullanılır.
		/// </summary>
		/// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Products.ToListAsync();
		}

        /// <summary>
		/// Belirli bir ürünü ID'sine göre asenkron olarak getirir.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<Product> GetByIdAsync(int id)
		{
			return await _context.Products.FindAsync(id);
		}

        /// <summary>
		/// Yeni bir ürünü asenkron olarak ekler. 
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
        public async Task AddAsync(Product product)
		{
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
		}

        /// <summary>
		/// Var olan bir ürünü asenkron olarak günceller.
		/// </summary>
		/// <param name="product"></param>
		/// <returns></returns>
        public async Task UpdateAsync(Product product)
		{
			_context.Products.Update(product);
			await _context.SaveChangesAsync();
		}

        /// <summary>
		/// Belirli bir ürünü (id``ye göre ) asenkron olarak siler. 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task DeleteAsync(int id)
		{
			var product = await GetByIdAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
			}
		}

        //Asenkron, bir işlemin tamamlanmasını beklemeden diğer işlemlerin devam etmesine olanak tanıyan bir programlama modelidir.
    }
}
