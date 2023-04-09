using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using OpenAI.GPT3.ObjectModels.ResponseModels.ImageResponseModel;

namespace OpenAI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {

        private readonly IOpenAIService openAIService;

        public OpenAIController(IOpenAIService openAIService)
        {
            this.openAIService = openAIService;
        }

        [HttpGet]
        [Route("askQuestion")]
        public async Task<ActionResult> GetChat(string question)
        {
            CompletionCreateResponse response = await openAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = question,
                MaxTokens = 500
            }, Models.TextDavinciV3);

            return Ok(response.Choices[0].Text);
        }

        [HttpGet]
        [Route("imageGenarate")]
        public async Task<ActionResult> GetImageGenerator(string imageGenarate)
        {
            ImageCreateResponse response = await openAIService.Image.CreateImage(new ImageCreateRequest()
            {
                Prompt = imageGenarate,
                N = 2,
                Size = StaticValues.ImageStatics.Size.Size256,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "Test"
            });
            return Ok(string.Join("\n\n", response.Results.Select(i => i.Url)));
        }

    }
}
