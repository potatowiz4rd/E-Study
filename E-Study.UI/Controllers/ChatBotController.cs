using E_Study.Service.chatbot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OpenAI_API;
using OpenAI_API.Chat;
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
        public async Task<ActionResult> GetChatResponse(string message)
        {
            var api = new OpenAIAPI("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");
            var chat = api.Chat.CreateConversation();
            chat.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = 0;

            /// give instruction as System
            chat.AppendSystemMessage("You are an assistant of a online studying website who helps teachers and students answer their questions.");

            // give a few examples as user and assistant
            chat.AppendUserInput("At which age children in Viet Name start going to school? five or six years old");
            chat.AppendExampleChatbotOutput("Six");
            chat.AppendUserInput("At which age student learn calculus?");
            chat.AppendExampleChatbotOutput("At the age of 16 to 17, advanced students on academic tracks may take calculus as an option.");

            // now let's ask it a question
            chat.AppendUserInput(message);
            // and get the response
            string response = await chat.GetResponseFromChatbotAsync();
         
            // the entire chat history is available in chat.Messages
            foreach (ChatMessage msg in chat.Messages)
            {
                Console.WriteLine($"{msg.Role}: {msg.Content}");
            }

            return Ok(response);
        }

    }

}

