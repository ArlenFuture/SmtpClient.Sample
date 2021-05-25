using Microsoft.Extensions.Configuration;
using SmtpClient.Sample.Constants;
using System;
using System.Net.Mail;

namespace SmtpClient.Sample.Services
{
    public class SmtpClientService
    {
        private readonly IConfiguration _configuration;

        #region ctor
        public SmtpClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        /// <summary>
        /// Send mail to specific smtp server.
        /// </summary>
        /// <param name="from">Sender</param>
        /// <param name="to">Recipient</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Content</param>
        /// <returns></returns>
        public string Send(string from, string to, string subject, string body)
        {
            //Initial Result
            string result = MethodsValue.Success;
            try
            {
                //Required
                MailMessage message = new MailMessage(from, to);

                //Optional
                message.Subject = subject;
                message.Body = body;

                //Setting from appsettings
                var smtpServerHost = _configuration.GetValue<string>(AppSettingsValue.SmtpServerHost);

                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpServerHost))
                {
                    // Credentials are necessary if the server requires the client
                    // to authenticate before it will send email on the client's behalf.
                    client.UseDefaultCredentials = true;

                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                result = string.Format("Error in SmtpClientService.Send(): {0}",
                    ex.Message);
            }

            return result;
        }
    }
}
