using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLACalculator.Application.Features.SLA.Queries;
using SLACalculator.Application.DTOs;

namespace SLACalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SlaController> _logger;

        public SlaController(IMediator mediator, ILogger<SlaController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("calculateSla")]
        [ProducesResponseType(typeof(CalculateSlaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CalculateSlaResponse>> CalculateSla([FromBody] CalculateSlaRequest request)
        {
            try
            {
                var query = new CalculateSlaQuery
                {
                    Priority = request.Priority,
                    CaptureDateTime = request.CaptureDateTime
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating SLA");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFiles([FromForm] IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded");

            var uploadedFiles = new List<string>();
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    uploadedFiles.Add(fileName);
                }
            }

            return Ok(new { files = uploadedFiles, count = uploadedFiles.Count });
        }
    }
}
