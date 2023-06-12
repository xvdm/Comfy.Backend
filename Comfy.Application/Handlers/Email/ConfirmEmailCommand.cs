using Comfy.Application.Common.Exceptions;
using Comfy.Application.Common.LocalizationStrings;
using Comfy.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Email;

public sealed record ConfirmEmailCommand(string Email, string ConfirmationCode) : IRequest<bool>;


public sealed class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private const int MaximumFailedAccessCount = 3;
    private readonly IApplicationDbContext _context;

    public ConfirmEmailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.PendingUsers.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user is null) throw new NotFoundException(LocalizationStrings.User);

        if (user.LockoutEnd is not null && user.LockoutEnd > DateTime.UtcNow) throw new UserEmailLockoutException();

        if (user.ConfirmationCode == request.ConfirmationCode)
        {
            _context.PendingUsers.Remove(user);
        }
        else
        {
            user.AccessFailedCount++;
            user.LockoutEnd = null;
            if (user.AccessFailedCount > MaximumFailedAccessCount)
            {
                user.LockoutEnd = DateTime.UtcNow.AddHours(1);
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return user.ConfirmationCode == request.ConfirmationCode;
    }
}