using MimeKit;

namespace ECommerce_API.Models.Email
{
    public class Message
    {
        public string To { get; set; } = null!;
        public string? Subject { get; set; } 
        public string? EmailContent { get; set; }
        
    }
}
