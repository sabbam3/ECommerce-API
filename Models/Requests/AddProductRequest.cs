namespace ECommerce_API.Models.Requests
{
    public class AddProductRequest
    {
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
