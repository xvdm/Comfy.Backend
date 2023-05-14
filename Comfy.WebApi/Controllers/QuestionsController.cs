using Comfy.Application.Handlers.Questions;
using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Handlers.Questions.Questions;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class QuestionsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetQuestions(int productId, int? pageNumber, int? pageSize)
    {
        var result = await Mediator.Send(new GetQuestionsWithAnswersQuery(productId, pageNumber, pageSize));
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

    [HttpPut("like")]
    public async Task<IActionResult> LikeQuestion(int questionId)
    {
        await Mediator.Send(new LikeQuestionCommand(questionId));
        return Ok();
    }

    [HttpPut("likeAnswer")]
    public async Task<IActionResult> LikeQuestionAnswer(int questionAnswerId)
    {
        await Mediator.Send(new LikeQuestionAnswerCommand(questionAnswerId));
        return Ok();
    }

    [HttpPut("dislike")]
    public async Task<IActionResult> DislikeQuestion(int questionId)
    {
        await Mediator.Send(new DislikeQuestionCommand(questionId));
        return Ok();
    }

    [HttpPut("dislikeAnswer")]
    public async Task<IActionResult> DislikeQuestionAnswer(int questionAnswerId)
    {
        await Mediator.Send(new DislikeQuestionAnswerCommand(questionAnswerId));
        return Ok();
    }
}