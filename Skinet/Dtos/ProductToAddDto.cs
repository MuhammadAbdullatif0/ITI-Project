namespace API.Dtos
{
    public class ProductToAddDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile ImgUrl { get; set; }
        public int ProductType { get; set; }
        public int ProductBrand { get; set; }
    }
}

