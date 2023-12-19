namespace ECommerce_API.Models.Requests
{
    public class EditProductRequest
    {
        public int ProductId { get; set; }
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
