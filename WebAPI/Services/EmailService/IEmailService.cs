using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cronicle.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailInfo emailInfo);
    }
}
