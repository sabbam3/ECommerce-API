using ECommerce_API.Abstractions;
using ECommerce_API.Models.Authentication.SignUp;
using ECommerce_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;


namespace ECommerce_API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CreateUserAsync(RegisterUser user)
        {
            var entity = new UserEntity();
            entity.Email = user.Email;
            entity.UserName = user.UserName;
            var result = await _userManager.CreateAsync(entity, user.Password);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }
       
    }
}
