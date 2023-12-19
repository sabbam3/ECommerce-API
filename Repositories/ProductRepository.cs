using ECommerce_API.Abstractions;
using ECommerce_API.Db;
using ECommerce_API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace ECommerce_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddProductAsync(ProductEntity entity)
        {
                await _db.Products
                      .AddAsync(entity);
        }
        public async Task AddFeedBackAsync(FeedbackEntity entity)
        {
            await _db.Feedbacks
                  .AddAsync(entity);
        }
        public async Task<List<ProductEntity>?> GetUserProductsAsync(UserEntity owner)
        {
            return await _db.Products
                        .Where(p => p.Owner == owner)
                        .ToListAsync();
        }
        public async Task<ProductEntity?> GetUserProductByIdAsync(UserEntity owner, int productId)
        {
            return await _db.Products
                        .Where(p => p.Owner == owner && p.Id == productId)
                        .FirstOrDefaultAsync();
        }
        public async Task<ProductEntity?> GetProductByIdAsync(int id)
        {
            return await _db.Products
                         .Include(o => o.Owner)
                         .Include(f => f.FeedBack)
                         .Where(p => p.Id == id)
                         .FirstOrDefaultAsync();
        }
        public async Task<List<ProductEntity>?> GetProductByTitleAsync(string title)
        {
            return await _db.Products
                         .Include(o => o.Owner)
                         .Include(f => f.FeedBack)
                         .Where(p => p.Title == title)
                         .ToListAsync();
        }
        public async Task<UserEntity?> GetUserAsync(string email)
        {
            return await _db.Users
                         .Where(u => u.Email == email)
                         .Include(p => p.OwnedProducts)
                         .FirstOrDefaultAsync();
        }
        public async Task<List<FeedbackEntity>?> GetFeedBacksByProductIdAsync(int productId)
        {
            return await _db.Feedbacks
                         .Include(p => p.Product)
                         .Where(f => f.ProductId == productId)
                         .ToListAsync();  
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
          
    }
}
