using ApiContestNew.Dtos.CoolFeature;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiContestNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoolController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCoolResult(CoolFeatureDto dto)
        {
            if (String.IsNullOrWhiteSpace(dto.CoolField) || String.IsNullOrWhiteSpace(dto.MegaCoolField))
            {
                return BadRequest("Not cool");
            }

            return Ok("Toooooooo coooooool");
        }
    }
}
