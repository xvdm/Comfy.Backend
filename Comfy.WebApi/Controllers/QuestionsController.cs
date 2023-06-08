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

    /// <summary>
    /// Returns questions for the product
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetQuestions(int productId, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetQuestionsQuery(productId, pageNumber, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Creates a question for the product || JwtValidation
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateQuestion(CreateQuestionCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Creates an answer to the question || JwtValidation
    /// </summary>
    [HttpPost("answer")]
    public async Task<IActionResult> CreateQuestionAnswer(CreateQuestionAnswerCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Increases the number of likes on the question || JwtValidation
    /// </summary>
    [HttpPut("like")]
    public async Task<IActionResult> LikeQuestion(int questionId, Guid userId)
    {
        await Sender.Send(new LikeQuestionCommand(questionId, userId));
        return Ok();
    }

    /// <summary>
    /// Increases the number of likes on the answer to the question || JwtValidation
    /// </summary>
    [HttpPut("likeAnswer")]
    public async Task<IActionResult> LikeQuestionAnswer(int questionAnswerId, Guid userId)
    {
        await Sender.Send(new LikeQuestionAnswerCommand(questionAnswerId, userId));
        return Ok();
    }

    /// <summary>
    /// Increases the number of dislikes on the question || JwtValidation
    /// </summary>
    [HttpPut("dislike")]
    public async Task<IActionResult> DislikeQuestion(int questionId, Guid userId)
    {
        await Sender.Send(new DislikeQuestionCommand(questionId, userId));
        return Ok();
    }

    /// <summary>
    /// Increases the number of dislikes on the answer to the question || JwtValidation
    /// </summary>
    [HttpPut("dislikeAnswer")]
    public async Task<IActionResult> DislikeQuestionAnswer(int questionAnswerId, Guid userId)
    {
        await Sender.Send(new DislikeQuestionAnswerCommand(questionAnswerId, userId));
        return Ok();
    }
}