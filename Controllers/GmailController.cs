using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailController : ControllerBase
    {
        private readonly GmailServiceHelper _gmailService;

        public GmailController(GmailServiceHelper gmailService)
        {
            _gmailService = gmailService;
        }

        [HttpGet("emails")]
        public async Task<IActionResult> GetEmails()
        {
            try
            {
                List<Message> emails = await _gmailService.GetEmailsAsync();
                return Ok(emails);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error fetching emails: {ex.Message}");
            }
        }
    }
}
