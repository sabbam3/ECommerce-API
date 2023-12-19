using ECommerce_API.Abstractions;
using ECommerce_API.Models.Authentication.SignUp;
using ECommerce_API.Models.Email;
using ECommerce_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Net;
using System.Net.Mail;

namespace ECommerce_API.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;
       
        public EmailService(EmailConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            
        }
        private async Task<bool> SendEmailAsync(Message message)
        {
            try
            {
                MailMessage mail = new MailMessage(_configuration.From, message.To, message.Subject, message.EmailContent);
                mail.IsBodyHtml = false; // Set to true if you are sending HTML content

                using (SmtpClient smtpClient = new SmtpClient(_configuration.SmtpServer))
                {
                    smtpClient.Port = _configuration.Port;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_configuration.UserName, _configuration.Password);
                    smtpClient.EnableSsl = true; // Use SSL/TLS encryption

                    await smtpClient.SendMailAsync(mail);
                    return true;
                }
            }
            catch (Exception)
            {
                // Log or handle exceptions accordingly
                return false;
            }
        }
        private async Task<string?> GenerateEmailConfirmationLink(UserEntity entity, RegisterUser user)
        {
            entity = await _userManager.FindByEmailAsync(user.Email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(entity);
            var confirmationLink = $"http://localhost:7164/auth/ConfirmEmail?email={entity.Email}&token={token}";
            return confirmationLink;
        }
        public async Task<bool> SendConfirmationLink(UserEntity entity, RegisterUser user)
        {
            if(user.Email == null) return false;
            var message = new Message()
            {
                EmailContent = await GenerateEmailConfirmationLink(entity, user),
                Subject = "Email Confirmation",
                To = user.Email
            };
            if (!await SendEmailAsync(message)) return false;
            return true;
        }
        
    }
}
