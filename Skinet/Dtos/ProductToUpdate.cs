﻿namespace API.Dtos
{
    public class ProductToUpdate
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile ImgUrl { get; set; }
       
    }
}
