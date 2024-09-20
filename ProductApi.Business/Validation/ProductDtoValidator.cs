using FluentValidation;
using ProductApi.Dto.Product;

namespace ProductApi.Business.Validation
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
	{

		/// <summary>
		/// Product için geçerlilik kuralları.
		/// </summary>
		public ProductDtoValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı zorunludur.");
			RuleFor(x => x.Brand).NotEmpty().WithMessage("Ürün markası zorunludur.");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün açıklaması zorunludur.");
			RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Ürün fiyatı sıfır veya daha büyük olmalıdır..");
			RuleFor(x => x.StockQuantity).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Stok miktarı sıfır veya daha büyük olmalıdır.");
			RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Ürün resmi zorunludur.");
		}
	}
}
