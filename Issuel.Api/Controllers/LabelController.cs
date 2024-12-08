using Issuel.Application.Common.Dto.Label;
using Issuel.Application.Common.Dto.Label.Requests;
using Issuel.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Issuel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LabelController : ControllerBase
{
    private readonly ILabelService _labelService;

    public LabelController(ILabelService labelService)
    {
        _labelService = labelService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LabelDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var label = await _labelService.GetByIdAsync(id, cancellationToken);

        return Ok(label);
    }

    [HttpGet]
    public async Task<ActionResult<LabelDto[]>> Get(CancellationToken cancellationToken)
    {
        var labels = await _labelService.GetAllAsync(cancellationToken);
        
        return Ok(labels);
    }

    [HttpPost]
    public async Task<ActionResult<LabelDto>> Post(CreateLabelRequest request, CancellationToken cancellationToken)
    {
        var label = await _labelService.CreateAsync(request, cancellationToken);
        
        return CreatedAtAction(nameof(Get), new { id = label.Id }, label);
    }

    [HttpPut]
    public async Task<ActionResult<LabelDto>> Update(UpdateLabelRequest request, CancellationToken cancellationToken)
    {
        var label = await _labelService.UpdateAsync(request, cancellationToken);
        
        return Ok(label);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _labelService.DeleteAsync(id, cancellationToken);
        
        return NoContent();
    }
}