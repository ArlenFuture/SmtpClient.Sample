using Microsoft.AspNetCore.Mvc;
using SmtpClient.Sample.Constants;
using SmtpClient.Sample.Models;
using SmtpClient.Sample.Services;

namespace SmtpClient.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmtpClientController : ControllerBase
    {
        private readonly SmtpClientService _smtpClientService;
        #region ctor
        public SmtpClientController(SmtpClientService smtpClientService)
        {
            _smtpClientService = smtpClientService;
        }
        #endregion

        [HttpPost("Send")]
        public IActionResult Send(Email email)
        {
            string result = _smtpClientService.Send(email.From, email.To, email.Subject, email.Body);
            return result == MethodsValue.Success ? Ok() : BadRequest(result);
        }
    }
}
