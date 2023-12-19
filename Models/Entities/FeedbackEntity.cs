namespace ECommerce_API.Models.Entities
{
    public class FeedbackEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Comment { get; set; }
        public float Rating { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;
    }
}
