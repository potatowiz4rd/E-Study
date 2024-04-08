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
    }

}

