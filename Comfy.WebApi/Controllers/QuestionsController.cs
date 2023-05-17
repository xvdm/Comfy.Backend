using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Handlers.Questions.Questions;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class QuestionsController : BaseController
{
    public QuestionsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions(int productId, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetQuestionsWithAnswersQuery(productId, pageNumber, pageSize));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion(CreateQuestionCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    [HttpPost("answer")]
    public async Task<IActionResult> CreateQuestionAnswer(CreateQuestionAnswerCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    [HttpPut("like")]
    public async Task<IActionResult> LikeQuestion(int questionId)
    {
        await Sender.Send(new LikeQuestionCommand(questionId));
        return Ok();
    }

    [HttpPut("likeAnswer")]
    public async Task<IActionResult> LikeQuestionAnswer(int questionAnswerId)
    {
        await Sender.Send(new LikeQuestionAnswerCommand(questionAnswerId));
        return Ok();
    }

    [HttpPut("dislike")]
    public async Task<IActionResult> DislikeQuestion(int questionId)
    {
        await Sender.Send(new DislikeQuestionCommand(questionId));
        return Ok();
    }

    [HttpPut("dislikeAnswer")]
    public async Task<IActionResult> DislikeQuestionAnswer(int questionAnswerId)
    {
        await Sender.Send(new DislikeQuestionAnswerCommand(questionAnswerId));
        return Ok();
    }
}