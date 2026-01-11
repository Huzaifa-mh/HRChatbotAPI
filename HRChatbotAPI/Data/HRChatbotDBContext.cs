using HRChatbotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HRChatbotAPI.Data
{
    public class HRChatbotDBContext(DbContextOptions<HRChatbotDBContext> options):DbContext (options)
    {
        public DbSet<Conversation> Conversations => Set<Conversation>();
    }
}
