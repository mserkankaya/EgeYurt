using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Business.Interfaces;
using ProductApi.Business.Validation;
using ProductApi.Dto.Product;
using ProductApi.Entity.Concrete;
using Serilog;

namespace ProductApi.Controllers
{
    /// <summary>
    /// ProductController, kullanıcıların ürün verileriyle etkileşimde bulunmasını sağlar. Kimlik doğrulama ve yetkilendirme mekanizmaları sayesinde, sadece yetkili kullanıcıların ürün ekleme, güncelleme ve silme işlemleri yapması sağlanır. 
    /// </summary>
	
    [Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		/// <summary>
		/// Tüm Ürünleri Getirir
		/// </summary>
		/// <returns></returns>
		/// 
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			Log.Information("Tüm ürünler getiriliyor.");
			return Ok(await _productService.GetAllProductsAsync());
		}

        /// <summary>
        /// Yeni bir ürün ekler
        /// </summary>
        /// <param name="productDto">Yeni ürün bilgilerini içeren DTO.</param>
        /// <returns></returns>
        [HttpPost]
		[Authorize]
		public async Task<ActionResult> PostProduct([FromBody] ProductDto productDto)
		{
			var validator = new ProductDtoValidator();
			var validationResult = await validator.ValidateAsync(productDto);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			var product = new Product
			{
				Name = productDto.Name,
				Brand = productDto.Brand,
				Description = productDto.Description,
				Price = productDto.Price,
				StockQuantity = productDto.StockQuantity,
				ImageUrl = productDto.ImageUrl,
				IsActive = productDto.IsActive,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			};
			Log.Information("Yeni ürün ekleniyor: {ProductName}", productDto.Name);
			await _productService.AddProductAsync(product);
			return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
		}

        /// <summary>
        /// Var olan bir ürünü günceller.
        /// </summary>
        /// <param name="id">Güncellenecek ürünün ID'si</param>
        /// <param name="productDto">Güncelleme bilgilerini içeren DTO.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDto productDto)
        {
            var validator = new ProductDtoValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var product = new Product
                {
                    Id = id,
                    Name = productDto.Name,
                    Brand = productDto.Brand,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    StockQuantity = productDto.StockQuantity,
                    ImageUrl = productDto.ImageUrl,
                    IsActive = productDto.IsActive,
                    UpdatedDate = DateTime.UtcNow
                };

                await _productService.UpdateProductAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("Ürün güncellenirken hata oluştu: {ErrorMessage}", ex.Message);
                return StatusCode(500, "Sunucu hatası oluştu.");
            }
        }

        /// <summary>
        /// Belirtilen bir ürünü siler.
        /// </summary>
        /// <param name="id">Silinecek ürünün ID'si.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Log.Information("Ürün siliniyor: {ProductId}", id);
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}

