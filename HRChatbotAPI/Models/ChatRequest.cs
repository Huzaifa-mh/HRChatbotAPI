namespace HRChatbotAPI.Models
{
    public class ChatRequest
    {
        public int Id { get; set; }
        public string? UserMessage { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
