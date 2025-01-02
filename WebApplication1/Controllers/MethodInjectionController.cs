using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("apiTest/[controller]")]
    [ApiController]
    public class MethodInjectionController : ControllerBase
    {
        public MethodInjectionController() { }

        [HttpGet("Msg", Name = "GetMsgByMethod")]
        public String getMsg([FromServices] IMethodInjectionService methodInjectionService) {
            String message = methodInjectionService.getMsgByMethod();
            return message;
        }
    }
}
