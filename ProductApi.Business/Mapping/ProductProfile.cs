using AutoMapper;
using ProductApi.Dto.Product;
using ProductApi.Entity.Concrete;

namespace ProductApi.Business.Mapping
{
    public class ProductProfile : Profile
	{

        /// <summary>
        ///  //Product nesnesini ProductDto nesnesine, ProductDto nesnesini Product nesnesine dönüştürür.
        /// </summary>
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();           
        }
    }
}
