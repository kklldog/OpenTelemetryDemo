using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Resources;
using System.Diagnostics;

namespace WebApplication2.Controllers
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public async Task<string> Login([FromBody] LoginModel model)
        {
            var user = await new UserRepository().GetUserAsync(model.Username, model.Password);
            if (user != null)
            {
                return "ok";
            }

            return "error";
        }

    }
}
