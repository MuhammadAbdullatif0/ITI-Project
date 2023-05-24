using System.ComponentModel.DataAnnotations;

namespace Core;

public class Product:BaseEntity
{
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string imgUrl { get; set; } = string.Empty;
    public ProductType? ProductType { get; set; }
    public int ProductTypeId { get; set; }
    public ProductBrand? ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
   

}
