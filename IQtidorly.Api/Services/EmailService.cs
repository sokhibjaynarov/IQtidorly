using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Emails;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class EmailService : BaseService, IEmailService
    {
        public EmailService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<EmailService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task SendEmailAsync(EmailViewModel viewModel)
        {
            string emailHost = _configuration.GetSection("EmailInfo:EmailHost").Value;
            int emailPort = int.Parse(_configuration.GetSection("EmailInfo:EmailPort").Value);
            string emailUsername = _configuration.GetSection("EmailInfo:EmailUsername").Value;
            string emailPassword = _configuration.GetSection("EmailInfo:EmailPassword").Value;

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(emailUsername));
            message.To.Add(MailboxAddress.Parse(viewModel.To));
            message.Subject = viewModel.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = viewModel.Body
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(emailHost, emailPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailUsername, emailPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch
            {
                await client.DisconnectAsync(true);
                throw;
            }
        }
    }
}
