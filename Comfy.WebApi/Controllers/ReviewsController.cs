using Comfy.Application.Handlers.Reviews;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class ReviewsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetReviews(int productId)
    {
        var result = await Mediator.Send(new GetReviewsQuery(productId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPost("answer")]
    public async Task<IActionResult> CreateReviewAnswer(CreateReviewAnswerCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}