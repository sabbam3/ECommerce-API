using ECommerce_API.Models.Authentication.SignUp;
using ECommerce_API.Models.Email;
using ECommerce_API.Models.Entities;

namespace ECommerce_API.Abstractions
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationLink(UserEntity entity, RegisterUser user);
    }
}
