namespace Core;

public class ProductSpecParam
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;
    private int _PageSize = 6;
    public int PageSize
    {
        get => _PageSize; 
        set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Sort { get; set; }
    private string search;
    public string Search { get => search; set => search = value.ToLower(); }
}
