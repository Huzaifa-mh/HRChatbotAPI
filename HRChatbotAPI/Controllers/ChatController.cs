using HRChatbotAPI.Data;
using HRChatbotAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
//using HRChatbotAPI.Models.GeminiResponse;

namespace HRChatbotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(HRChatbotDBContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration) : ControllerBase
    {
        private readonly HRChatbotDBContext _context = context;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("History")]
        public async Task<List<Conversation>> GetHistory()
        {
            var data = await _context.Conversations.OrderBy(c =>c.TimeStamp).ToListAsync();
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

        //Creating a Post Request to send user message and get response from Gemini API
        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message cant be Empty");
            }

            var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["GeminiApiKey"];
            var geminiUrl = $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new {text = $"You are an HR assitant. Help the user with: {request.Message}"}
                        }
                    }
                }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json");
            try
            {
                var response = await httpClient.PostAsync(geminiUrl, jsonContent);
                var responseString = await response.Content.ReadAsStringAsync();

                //Debug
                Console.WriteLine($"Gemini Response: {responseString}");
                var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true});


                var aiResponse = geminiResponse?.Candidates?[0]?.Content?.Parts?[0]?.Text ?? "Sorry, I couldn't generate a response.";

                var conversation = new Conversation
                {
                    UserMessage = request.Message,
                    AIResponse = aiResponse
                };

                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();

                return Ok(new { response = aiResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

            
        }
    }
}
