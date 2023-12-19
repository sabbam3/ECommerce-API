namespace ECommerce_API.Models.Entities
{
    public class ProductEntity
    {
      public int Id { get; set; }
      public int UserId { get; set; }
      public string Title { get; set; } = null!;
      public double Price { get; set; }
      public string? Description { get; set; }
      public DateTime PlacedDate { get; set; }
      public DateTime? LastModifiedDate { get; set; }
      public UserEntity Owner { get; set; } = null!;
      public List<FeedbackEntity>? FeedBack { get; set; }

    }
}
