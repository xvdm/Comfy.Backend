using Comfy.Application.Services.Email;
using MediatR;

namespace Comfy.Application.Handlers.Email;

public sealed record SendConfirmationEmailCommand(string Email, string ConfirmationCode) : IRequest;


public sealed class SendConfirmationEmailCommandHandler : IRequestHandler<SendConfirmationEmailCommand>
{
    private readonly IEmailService _emailService;

    public SendConfirmationEmailCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var subject = "LOFFY | Підтвердження пошти";
        var message = $"Ваш код підтвердження: {request.ConfirmationCode}";
        await _emailService.SendEmailAsync(request.Email, subject, message);
    }
}