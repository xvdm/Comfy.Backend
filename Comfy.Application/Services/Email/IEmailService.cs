﻿namespace Comfy.Application.Services.Email;

public interface IEmailService
{
    public Task SendEmailAsync(string email, string subject, string message);
}