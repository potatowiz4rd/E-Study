using E_Study.Service.chatbot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OpenAI_API;
using OpenAI_API.Completions;
using System.Threading.Tasks;

namespace E_Study.UI.Controllers
{
    public class ChatBotController : ControllerBase
    {
        private readonly OpenAIChatGPTService _chatGptService;

        public ChatBotController(OpenAIChatGPTService chatGptService)
        {
            _chatGptService = chatGptService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> GetChatResponse([FromBody] string message)
        {
            var response = await _chatGptService.GetChatResponse(message);
            return Ok(response);
        }

        [HttpPost]
        [Route("getanswer")]
        public async Task<IActionResult> GetResult([FromBody] string prompt)
        {
            try
            {
                // Your OpenAI API key
                var api = new OpenAIAPI("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");

                var chat = api.Chat.CreateConversation();
                chat.AppendUserInput("How to make a hamburger?");

                await chat.StreamResponseFromChatbotAsync(res =>
                {
                    Console.Write(res);
                });
                return Ok();
            }
            catch (Exception ex)
            {
                // Return a bad request with the error message
                return BadRequest($"Error: {ex.Message}");
            }
        }



    }

}

