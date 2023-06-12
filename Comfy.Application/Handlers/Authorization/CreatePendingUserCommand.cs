﻿using Comfy.Application.Common.Exceptions;
using Comfy.Application.Interfaces;
using Comfy.Application.Services.Email;
using Comfy.Domain.Entities;
using Comfy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comfy.Application.Handlers.Authorization;

public sealed record CreatePendingUserCommand(string Email) : IRequest<string>;


public sealed class CreatePendingUserCommandHandler : IRequestHandler<CreatePendingUserCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public CreatePendingUserCommandHandler(UserManager<User> userManager, IApplicationDbContext context, IEmailService emailService)
    {
        _userManager = userManager;
        _context = context;
        _emailService = emailService;
    }

    public async Task<string> Handle(CreatePendingUserCommand request, CancellationToken cancellationToken)
    {
        var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithEmail is not null) throw new UserWithGivenEmailAlreadyExistsException();

        var pendingUsersCount = await _context.PendingUsers.CountAsync(x => x.Email == request.Email, cancellationToken);
        if (pendingUsersCount > 0) throw new UserWithGivenEmailAlreadyExistsException();

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