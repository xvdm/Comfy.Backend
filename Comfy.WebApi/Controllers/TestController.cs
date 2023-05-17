using Comfy.WebApi.Controllers.Base;
using MediatR;

namespace Comfy.WebApi.Controllers;

public sealed class TestController : BaseController
{
    public TestController(ISender sender) : base(sender)
    {
    }

    //private readonly IApplicationDbContext _context;
    //public TestController(IApplicationDbContext context)
    //{
    //    _context = context;
    //}
    //
    //[HttpGet]
    ////[Time]
    //public async Task<IActionResult> Test(CancellationToken cancellationToken)
    //{
    //    return Ok();
    //}
}