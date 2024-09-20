using Microsoft.EntityFrameworkCore;
using ProductApi.Entity.Concrete;

namespace ProductApi.DataAccess.Concrete
{
	public class EgeYurtContext : DbContext
	{

        /// <summary>
		/// Veritabanı bağlantı dizesi
		/// </summary>
		/// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			     optionsBuilder.UseSqlServer("Server=localhost,1445;Database=EgeYurt;User=sa;Password=Mskdev2363+");

		}

		public EgeYurtContext(DbContextOptions<EgeYurtContext> options) : base(options) { }

        /// <summary>
		/// Product nesnelerini temsil eden veritabanı seti.
		/// </summary>
        public DbSet<Product> Products { get; set; }
	}
	
}
