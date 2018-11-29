using System;
using System.Collections.Generic;
using System.Text;

namespace Educo.Parking.Business.Services.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
