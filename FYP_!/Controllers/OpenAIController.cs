using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;

namespace FYP__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("UseChatGPT")]
        public async Task<IActionResult> UseChatGPT(string query)
        {
            string outputResult = "";
            var openai = new OpenAIAPI("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");
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = query;
            completionRequest.Model = OpenAI_API.Models.Model.ChatGPTTurboInstruct;
            completionRequest.MaxTokens = 1024;

            var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                outputResult += completion.Text;
            }

            return Ok(outputResult);

        }
    }

}
