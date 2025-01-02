using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("apiTest/[controller]")]
    [ApiController]
    public class LifeCycleController(ITransientTestService transientService, ITransientTestService transientService1, IScopedTestService scopedService, IScopedTestService scopedService1, ISingletonTestService singletonService) : ControllerBase
    {
        private readonly ITransientTestService _transientService = transientService;
        private readonly ITransientTestService _transientService1 = transientService1;
        private readonly IScopedTestService _scopedService = scopedService;
        private readonly IScopedTestService _scopedService1 = scopedService1;
        private readonly ISingletonTestService _singletonService = singletonService;

        [HttpGet("test-lifecycle")]
        public IActionResult TestLifecycle()
        {
            return Ok(new
            {
                TransientId = _transientService.GetId(),
                Transient1Id = _transientService1.GetId(),
                ScopedId = _scopedService.GetId(),
                ScopedId1 = _scopedService1.GetId(),
                SingletonId = _singletonService.GetId(),
                TrEqTr1 = _transientService.GetId() == _transientService1.GetId(),
                ScEqSc1 = _scopedService.GetId() == _scopedService1.GetId(),
                TrEqSc = _transientService.GetId() == _scopedService.GetId(),
                TrEqSi = _transientService.GetId() == _singletonService.GetId()
            });
        }
    }
}
