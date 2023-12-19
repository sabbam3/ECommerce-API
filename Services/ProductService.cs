using ECommerce_API.Abstractions;
using ECommerce_API.Models.Dtos;
using ECommerce_API.Models.Entities;
using ECommerce_API.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly UserManager<UserEntity> _userManager;
        public ProductService(IProductRepository repository, UserManager<UserEntity> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task AddProductAsync(AddProductRequest request, UserEntity user)
        {
            ProductEntity entity = new ProductEntity();
            entity.UserId = user.Id;
            entity.Title = request.Title;
            entity.Price = request.Price;
            entity.Description = request.Description;
            entity.PlacedDate = DateTime.UtcNow;
            entity.LastModifiedDate = DateTime.UtcNow;
            await _repository.AddProductAsync(entity);
            await _repository.SaveChangesAsync();
        }
        public async Task<bool> AddFeedBackAsync(AddFeedBackRequest request, string userName)
        {
            FeedbackEntity entity = new FeedbackEntity();
            var product = await _repository.GetProductByIdAsync(request.ProductId);
            if(product == null)  return false;
            entity.Product = product;
            entity.ProductId = product.Id;
            entity.UserName = userName;
            entity.Comment = request.Comment;
            entity.Rating = request.Rating;
            await _repository.AddFeedBackAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditProductAsync(EditProductRequest request, UserEntity entity)
        {
            var product = await _repository.GetUserProductByIdAsync(entity, request.ProductId);
            if (product != null)
            {
                product.Title = request.Title;
                product.Price = request.Price;
                product.Description = request.Description;
                product.LastModifiedDate = DateTime.UtcNow;
                await _repository.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public async Task<List<ProductEntity>?> GetUserProductsAsync(UserEntity entity)
        {
            return await _repository.GetUserProductsAsync(entity);
        }
        public async Task<ProductEntity?> GetProductByIdAsync(int id)
        {
            return await _repository.GetProductByIdAsync(id);
            
        }
        public async Task<List<ProductEntity>?> GetProductByTitleAsync(string title)
        {
            return await _repository.GetProductByTitleAsync(title);
        }
        public async Task<List<FeedbackEntity>?> GetFeedBacksByProductIdAsync(int productId)
        {
            return await _repository.GetFeedBacksByProductIdAsync(productId);
        }



    }
}
