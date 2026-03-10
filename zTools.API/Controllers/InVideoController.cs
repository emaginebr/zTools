using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zTools.Domain.Services.Interfaces;
using zTools.DTO.InVideo;
using System;
using System.Threading.Tasks;

namespace zTools.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InVideoController : ControllerBase
    {
        private readonly IInVideoService _inVideoService;
        private readonly ILogger<InVideoController> _logger;

        public InVideoController(IInVideoService inVideoService, ILogger<InVideoController> logger)
        {
            _inVideoService = inVideoService;
            _logger = logger;
        }

        [HttpPost("generateVideo")]
        public async Task<ActionResult<InVideoResponse>> GenerateVideo([FromBody] InVideoRequest request)
        {
            try
            {
                _logger.LogInformation("Generating video with InVideo AI");
                var response = await _inVideoService.GenerateVideoAsync(request);
                _logger.LogInformation("InVideo AI video generated successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating video with InVideo AI");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
