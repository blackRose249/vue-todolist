using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    [Route("apiTest/[controller]")]
    [ApiController]
    public class ConstructorInjectionController(IConstructorInjectionService constructorInjectionService) : ControllerBase
    {
        private readonly IConstructorInjectionService constructorInjectionService = constructorInjectionService;

        [HttpGet("Msg", Name = "GetMsg")]
        public String GetData() { 
            return constructorInjectionService.getMsg();
        }
    }
}
