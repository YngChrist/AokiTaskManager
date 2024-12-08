using Issuel.Application.Common.Dto.Issue;
using Issuel.Application.Common.Dto.Issue.Requests;
using Issuel.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Issuel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssueController : ControllerBase
{
    private readonly IIssueService _issueService;

    public IssueController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IssueDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var issue = await _issueService.GetByIdAsync(id, cancellationToken);
        
        return Ok(issue);
    }

    [HttpGet]
    public async Task<ActionResult<PreviewIssueDto>> GetAllInOrder(CancellationToken cancellationToken)
    {
        var issues = await _issueService.GetAllAsync(cancellationToken);
        
        return Ok(issues);
    }
    
    [HttpGet("undone")]
    public async Task<ActionResult<PreviewIssueDto[]>> GetUndone(CancellationToken cancellationToken)
    {
        var issues = await _issueService.GetAllUndoneAsync(cancellationToken);
        
        return Ok(issues);
    }
    
    [HttpGet("done")]
    public async Task<ActionResult<PreviewIssueDto[]>> GetDone(CancellationToken cancellationToken)
    {
        var issues = await _issueService.GetAllDoneAsync(cancellationToken);
        
        return Ok(issues);
    }

    [HttpPost]
    public async Task<ActionResult<IssueDto>> Create(CreateIssueRequest request, CancellationToken cancellationToken)
    {
        var issue = await _issueService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = issue.Id }, issue);
    }

    [HttpPut]
    public async Task<ActionResult<IssueDto>> Update(UpdateIssueRequest request, CancellationToken cancellationToken)
    {
        var issue = await _issueService.UpdateAsync(request, cancellationToken);
        
        return Ok(issue);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _issueService.DeleteAsync(id, cancellationToken);
        
        return NoContent();
    }
}