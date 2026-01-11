using HRChatbotAPI.Data;
using HRChatbotAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRChatbotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(HRChatbotDBContext context) : ControllerBase
    {
        private readonly HRChatbotDBContext _context = context;

        [HttpGet("History")]
        public async Task<List<Conversation>> GetHistory()
        {
            var data = await _context.Conversations.OrderBy(c =>c.TimeStamp).ToListAsync();
            data.OrderBy(c => c.TimeStamp);

            return (data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConversationByID(int id)
        {
            var userData = await _context.Conversations.FindAsync(id);

            if(userData == null)
            {
                return NotFound();
            }
            return Ok(userData);
        }
    }
}
