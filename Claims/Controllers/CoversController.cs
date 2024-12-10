using Microsoft.AspNetCore.Mvc;

using Claims.Service;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly ILogger<CoversController> _logger;
    private readonly ICoversService _coversService;
    
    public CoversController(
        ICoversService coversService,
        ILogger<CoversController> logger
        )
    {
        _logger = logger;
        _coversService = coversService;
    }

    [HttpPost("compute")]
    public async Task<ActionResult> ComputePremiumAsync(DateTime startDate, DateTime endDate, CoverType coverType)
    {
        var foo = await Task.Run(() => Premium.ComputePremium(startDate, endDate, coverType));
        return Ok(foo);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
      var result=await _coversService.GetAsync();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
      var result=await _coversService.GetAsync(id);
      return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Interchange.Cover cover)
    {
      await _coversService.CreateAsync(cover);
      return Ok(cover);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        await _coversService.DeleteAsync(id);
    }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync()
        {
            var result = await _coversService.DeleteAsync();

          if (result.Success)
            return Ok();
          else
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }
}
