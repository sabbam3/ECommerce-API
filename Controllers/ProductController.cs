using ECommerce_API.Abstractions;
using ECommerce_API.Models.Entities;
using ECommerce_API.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IProductService _service;
        public ProductController(UserManager<UserEntity> userManager, IProductService service)
        {
            _userManager = userManager;
            _service = service;
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductAsync(AddProductRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            else
            {
                await _service.AddProductAsync(request, user);
                return Ok();
            }
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("add-feedback")]
        public async Task<IActionResult> AddFeedBackAsync(AddFeedBackRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            if (!await _service.AddFeedBackAsync(request, user.UserName)) return BadRequest("Could not add feedback");
            else return Ok();
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("edit-product")]
        public async Task<IActionResult> EditProductAsync([FromBody]EditProductRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            if (!await _service.EditProductAsync(request, user)) return NotFound("Product not found");
            else return Ok();
        }
        [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("get-user-product")]
        public async Task<IActionResult> GetUserProductsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found");
            else
            {
                return Ok(await _service.GetUserProductsAsync(user));
            }
        }
        [AllowAnonymous]
        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            if (await _service.GetProductByIdAsync(id) == null) return NotFound("Product not found");
            else return Ok(await _service.GetProductByIdAsync(id));
        }
        [AllowAnonymous]
        [HttpGet("get-products-by-title")]
        public async Task<IActionResult> GetProductByTitleAsync(string title)
        {
            var product = await _service.GetProductByTitleAsync(title);
            if (product.IsNullOrEmpty()) return NotFound("Product not found");
            else return Ok(product);
        }
        [AllowAnonymous]
        [HttpGet("get-feedbacks-by-product-id")]
        public async Task<IActionResult> GetFeedbacksByProductIdAsync(int id)
        {
            var feedbacks = await _service.GetFeedBacksByProductIdAsync(id);
            if (feedbacks.IsNullOrEmpty()) return NotFound("Feedback not found");
            else return Ok(feedbacks);
        }
    }
}
