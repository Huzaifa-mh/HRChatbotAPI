namespace HRChatbotAPI.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string? UserMessage { get; set; }
        public string? ApiResponse { get; set; }
        
        public DateTime TimeStamp { get; set; }
    }
}
