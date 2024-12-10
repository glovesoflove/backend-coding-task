using Microsoft.AspNetCore.Mvc;

using Claims.Service;

namespace Claims.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimsService _claimsService;

        public ClaimsController(
          ILogger<ClaimsController> logger,
          IClaimsService claimsService
          )
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync()
        {
            var result = await _claimsService.DeleteAsync();

          if (result.Success)
            return Ok();
          else
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }

        [HttpGet]
        public async Task<IEnumerable<Claim>> GetAsync()
        {
          return await _claimsService.GetClaimsAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Interchange.Claim claim)
        {
            await _claimsService.AddItemAsync(claim);
            
            return Ok(claim);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _claimsService.DeleteItemAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<Claim> GetAsync(string id)
        {
            return await _claimsService.GetClaimAsync(id);
        }
    }
}
