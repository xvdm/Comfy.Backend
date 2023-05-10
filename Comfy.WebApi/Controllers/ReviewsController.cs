using Comfy.Application.Handlers.Reviews;
using Comfy.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public class ReviewsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetReviews(int productId, int? pageNumber, int? pageSize)
    {
        var result = await Mediator.Send(new GetReviewsQuery(productId, pageNumber, pageSize));
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

    [HttpPut("like")]
    public async Task<IActionResult> LikeReview(int reviewId)
    {
        await Mediator.Send(new LikeReviewCommand(reviewId));
        return Ok();
    }

    [HttpPut("likeAnswer")]
    public async Task<IActionResult> LikeReviewAnswer(int reviewAnswerId)
    {
        await Mediator.Send(new LikeReviewAnswerCommand(reviewAnswerId));
        return Ok();
    }

    [HttpPut("dislike")]
    public async Task<IActionResult> DislikeReview(int reviewId)
    {
        await Mediator.Send(new DislikeReviewCommand(reviewId));
        return Ok();
    }

    [HttpPut("dislikeAnswer")]
    public async Task<IActionResult> DislikeReviewAnswer(int reviewAnswerId)
    {
        await Mediator.Send(new DislikeReviewAnswerCommand(reviewAnswerId));
        return Ok();
    }
}