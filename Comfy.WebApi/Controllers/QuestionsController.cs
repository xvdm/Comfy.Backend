using Comfy.Application.Handlers.Questions;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class QuestionsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetQuestions(int productId)
    {
        var result = await Mediator.Send(new GetQuestionsQuery(productId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion(CreateQuestionCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPost("answer")]
    public async Task<IActionResult> CreateQuestionAnswer(CreateQuestionAnswerCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}