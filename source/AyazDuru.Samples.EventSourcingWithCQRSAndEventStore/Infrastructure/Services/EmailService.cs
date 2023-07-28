using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Services
{
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {            
            return Task.CompletedTask;
        }
    }
}
