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

    /// <summary>
    /// Returns reviews for the product
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetReviews(int productId, int? pageNumber, int? pageSize)
    {
        var result = await Sender.Send(new GetReviewsQuery(productId, pageNumber, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Creates a review for the product || JwtValidation
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Creates an answer to the review || JwtValidation
    /// </summary>
    [HttpPost("answer")]
    public async Task<IActionResult> CreateReviewAnswer(CreateReviewAnswerCommand command)
    {
        await Sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Increases the number of likes on the review || JwtValidation
    /// </summary>
    [HttpPut("like")]
    public async Task<IActionResult> LikeReview(int reviewId, Guid userId)
    {
        await Sender.Send(new LikeReviewCommand(reviewId, userId));
        return Ok();
    }

    /// <summary>
    /// Increases the number of likes on the answer to the review || JwtValidation
    /// </summary>
    [HttpPut("likeAnswer")]
    public async Task<IActionResult> LikeReviewAnswer(int reviewAnswerId, Guid userId)
    {
        await Sender.Send(new LikeReviewAnswerCommand(reviewAnswerId, userId));
        return Ok();
    }

    /// <summary>
    /// Increases the number of dislikes on the review || JwtValidation
    /// </summary>
    [HttpPut("dislike")]
    public async Task<IActionResult> DislikeReview(int reviewId, Guid userId)
    {
        await Sender.Send(new DislikeReviewCommand(reviewId, userId));
        return Ok();
    }

    /// <summary>
    /// Increases the number of dislikes on the answer to the review || JwtValidation
    /// </summary>
    [HttpPut("dislikeAnswer")]
    public async Task<IActionResult> DislikeReviewAnswer(int reviewAnswerId, Guid userId)
    {
        await Sender.Send(new DislikeReviewAnswerCommand(reviewAnswerId, userId));
        return Ok();
    }
}