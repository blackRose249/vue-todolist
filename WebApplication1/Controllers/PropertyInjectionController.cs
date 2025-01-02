using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("apiTest/[controller]")]
    [ApiController]
    public class PropertyInjectionController : ControllerBase
    {
        [FromServices]
        public IPropertyInjectionService PropertyInjectionService { get; set; }
        public PropertyInjectionController() { }

        [HttpGet("Msg", Name = "GetMsgByProperty")]
        public string GetMsg()
        {
            return PropertyInjectionService.getMsgByProperty();
        }
    }
}
