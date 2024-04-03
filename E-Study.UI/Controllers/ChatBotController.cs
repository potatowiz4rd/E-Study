using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;

namespace E_Study.UI.Controllers
{
    public class ChatBotController : Controller
    {
        [HttpPost]
        [Route("getanswer")]
        public IActionResult GetResult([FromBody] string prompt)
        {
            //your OpenAI API key
            string apiKey = "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";
            string answer = string.Empty;
            var openai = new OpenAIAPI(apiKey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = prompt;
            completion.Model = OpenAI_API.Models.Model.Davinci;
            completion.MaxTokens = 400;
            var result = openai.Completions.CreateCompletionAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
                return Ok(answer);
            }
            else
            {
                return BadRequest("Not found");
            }
        }
    }
}
