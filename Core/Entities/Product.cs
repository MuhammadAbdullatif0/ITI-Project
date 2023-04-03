using System.ComponentModel.DataAnnotations;

namespace Core;

public class Product
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }=string.Empty;
}
