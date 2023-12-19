using ECommerce_API.Models.Entities;
using ECommerce_API.Models.Requests;

namespace ECommerce_API.Abstractions
{
    public interface IProductService
    {
        Task AddProductAsync(AddProductRequest request, UserEntity entity);
        Task<bool> AddFeedBackAsync(AddFeedBackRequest request, string userName);
        Task<List<ProductEntity>?> GetUserProductsAsync(UserEntity entity);
        Task<ProductEntity?> GetProductByIdAsync(int id);
        Task<List<ProductEntity>?> GetProductByTitleAsync(string title);
        Task<List<FeedbackEntity>?> GetFeedBacksByProductIdAsync(int productId);
        Task<bool> EditProductAsync(EditProductRequest request, UserEntity entity);
    }
}
