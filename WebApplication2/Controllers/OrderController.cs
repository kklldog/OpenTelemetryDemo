using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public string Add()
        {
            MyMeterService.MyOrderCounter.Add(1);

            return "ok";
        }

    }
}
