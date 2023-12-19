using ECommerce_API.Models.Authentication.SignUp;
using ECommerce_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Abstractions
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(RegisterUser user);
    }
}
