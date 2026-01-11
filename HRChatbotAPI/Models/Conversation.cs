using System.Data;

namespace HRChatbotAPI.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string? UserMessage { get; set; }
        public string? AIResponse { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
