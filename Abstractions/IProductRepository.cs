using ECommerce_API.Models.Entities;

namespace ECommerce_API.Abstractions
{
    public interface IProductRepository
    {
        Task AddProductAsync(ProductEntity entity);
        Task AddFeedBackAsync(FeedbackEntity entity);
        Task<List<ProductEntity>?> GetUserProductsAsync(UserEntity entity);
        Task<ProductEntity?> GetUserProductByIdAsync(UserEntity owner, int productId);
        Task<UserEntity?> GetUserAsync(string email);
        Task<ProductEntity?> GetProductByIdAsync(int id);
        Task<List<ProductEntity>?> GetProductByTitleAsync(string title);
        Task<List<FeedbackEntity>?> GetFeedBacksByProductIdAsync(int productId);
        Task SaveChangesAsync();

    }
}
