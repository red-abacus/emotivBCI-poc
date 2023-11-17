using Emotiv.Services.ExpressionsInterpreter;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Produces("application/json")]
    [Route("api/receivingData")]
    public class ReceivingDataController : Controller
    {
        private readonly IExpressionsInterpreterService _expressionsInterpreterService;

        public ReceivingDataController(IExpressionsInterpreterService expressionsInterpreterService)
        {
            _expressionsInterpreterService = expressionsInterpreterService;
        }

        [HttpPatch("start")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> StartAnalizing()
        {
            Configurations.AnalizeData = true;
            _expressionsInterpreterService.StartAnalizing();
            await Task.Delay(TimeSpan.FromSeconds(Configurations.ProcessingTimeSeconds));
            var result = _expressionsInterpreterService.StopAnalizing();
            Configurations.AnalizeData = false;
            return Ok(result);
        }
    }
}