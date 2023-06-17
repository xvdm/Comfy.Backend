using Comfy.Application.Interfaces;
using Comfy.Application.Services.Email;
using Comfy.Domain.Entities;
using MediatR;

namespace Comfy.Application.Handlers.Authorization;

public sealed record CreatePendingUserCommand(string Email) : IRequest<string>;


public sealed class CreatePendingUserCommandHandler : IRequestHandler<CreatePendingUserCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public CreatePendingUserCommandHandler(IApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<string> Handle(CreatePendingUserCommand request, CancellationToken cancellationToken)
    {
        var confirmationCode = await _emailService.GenerateEmailConfirmationCodeAsync();

        var user = new PendingUser
        {
            Email = request.Email,
            ConfirmationCode = confirmationCode
        };

        _context.PendingUsers.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return confirmationCode;
    }
}