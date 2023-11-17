using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Produces("application/json")]
    [Route("api/receivingData")]
    public class ReceivingDataController : Controller
    {

        [HttpPatch("start")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> StartAnalizing()
        {
            Configurations.AnalizeData = true;
            await Task.Delay(TimeSpan.FromSeconds(Configurations.ProcessingTimeSeconds));
            Configurations.AnalizeData = false;
            return Ok();
        }
    }
}