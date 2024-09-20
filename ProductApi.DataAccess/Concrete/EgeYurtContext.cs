using Microsoft.EntityFrameworkCore;
using ProductApi.Entity.Concrete;

namespace ProductApi.DataAccess.Concrete
{
	public class EgeYurtContext : DbContext
	{
		          
		public EgeYurtContext(DbContextOptions<EgeYurtContext> options) : base(options) { }

        /// <summary>
		/// Product nesnelerini temsil eden veritabanı seti.
		/// </summary>
        public DbSet<Product> Products { get; set; }
	}
	
}
