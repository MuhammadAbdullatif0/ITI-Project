namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string imgUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            imgUrl = imgUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string imgUrl { get; set; }
    }
}