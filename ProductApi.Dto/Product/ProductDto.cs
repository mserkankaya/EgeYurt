namespace ProductApi.Dto.Product
{
    public class ProductDto
    {
        /// <summary>
        /// ürün bilgilerini taşımak için kullanılan bir DTO.
        /// </summary>

        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
