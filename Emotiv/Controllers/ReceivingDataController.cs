using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Produces("application/json")]
    [Route("api/receivingData")]
    public class ReceivingDataController : Controller
    {

        [HttpPatch()]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult StartAnalizing()
        {
            Configurations.AnalizeData = true;
            return Ok(true);
        }

        [HttpPatch()]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public IActionResult StopAnalizing()
        {
            Configurations.AnalizeData = false;
            return Ok(false);
        }
    }
}