using Comfy.Application.Handlers.Reviews.ReviewAnswers;
using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.WebApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comfy.WebApi.Controllers;

public sealed class ReviewsController : BaseController
{
    public ReviewsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(int productId, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetReviewsQuery(productId, pageNumber, pageSize));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    [HttpPost("answer")]
    public async Task<IActionResult> CreateReviewAnswer(CreateReviewAnswerCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    [HttpPut("like")]
    public async Task<IActionResult> LikeReview(int reviewId, Guid userId)
    {
        await Sender.Send(new LikeReviewCommand(reviewId, userId));
        return Ok();
    }

    [HttpPut("likeAnswer")]
    public async Task<IActionResult> LikeReviewAnswer(int reviewAnswerId, Guid userId)
    {
        await Sender.Send(new LikeReviewAnswerCommand(reviewAnswerId, userId));
        return Ok();
    }

    [HttpPut("dislike")]
    public async Task<IActionResult> DislikeReview(int reviewId, Guid userId)
    {
        await Sender.Send(new DislikeReviewCommand(reviewId, userId));
        return Ok();
    }

    [HttpPut("dislikeAnswer")]
    public async Task<IActionResult> DislikeReviewAnswer(int reviewAnswerId, Guid userId)
    {
        await Sender.Send(new DislikeReviewAnswerCommand(reviewAnswerId, userId));
        return Ok();
    }
}