namespace ECommerce_API.Models.Requests
{
    public class AddFeedBackRequest

    {
        public int ProductId { get; set; }
        public string? Comment { get; set; }
        public float Rating { get; set; }
    }
}
