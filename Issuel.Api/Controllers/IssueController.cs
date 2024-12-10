using Issuel.Api.Common.Dto;
using Issuel.Application.Common.Dto.Issue;
using Issuel.Application.Common.Dto.Issue.Requests;
using Issuel.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Issuel.Api.Controllers;

/// <summary>
/// Контроллер задач.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
public class IssueController : ControllerBase
{
    private readonly IIssueService _issueService;

    /// <summary>
    /// Контроллер задач.
    /// </summary>
    public IssueController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    /// <summary>
    /// Получение задачи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IssueDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var issue = await _issueService.GetByIdAsync(id, cancellationToken);
        
        return Ok(issue);
    }

    /// <summary>
    /// Получение всех задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список задач.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PreviewIssueDto>> GetAllInOrder(CancellationToken cancellationToken)
    {
        var issues = await _issueService.GetAllAsync(cancellationToken);
        
        return Ok(issues);
    }
    
    /// <summary>
    /// Получение всех невыполненных задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список невыполненных задач.</returns>
    [HttpGet("undone")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PreviewIssueDto[]>> GetUndone(CancellationToken cancellationToken)
    {
        var issues = await _issueService.GetAllUndoneAsync(cancellationToken);
        
        return Ok(issues);
    }
    
    /// <summary>
    /// Получение всех выполненных задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список выполненных задач.</returns>
    [HttpGet("done")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PreviewIssueDto[]>> GetDone(CancellationToken cancellationToken)
    {
        var issues = await _issueService.GetAllDoneAsync(cancellationToken);
        
        return Ok(issues);
    }

    /// <summary>
    /// Создание новой задачи.
    /// </summary>
    /// <param name="request">Запрос на создание задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Созданная задача.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<IssueDto>> Create([FromBody] CreateIssueRequest request, CancellationToken cancellationToken)
    {
        var issue = await _issueService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = issue.Id }, issue);
    }

    /// <summary>
    /// Обновление существующей задачи.
    /// </summary>
    /// <param name="request">Запрос на обновление задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновленная задача.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IssueDto>> Update([FromBody] UpdateIssueRequest request, CancellationToken cancellationToken)
    {
        var issue = await _issueService.UpdateAsync(request, cancellationToken);
        
        return Ok(issue);
    }

    /// <summary>
    /// Удаление задачи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _issueService.DeleteAsync(id, cancellationToken);
        
        return NoContent();
    }
}