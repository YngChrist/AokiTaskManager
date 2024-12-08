using Issuel.Application.Common.Dto.Issue;
using Issuel.Application.Common.Dto.Issue.Requests;

namespace Issuel.Application.Common.Interfaces;

public interface IIssueService
{
    Task<IssueDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PreviewIssueDto[]> GetAllUndoneAsync(CancellationToken cancellationToken = default);
    Task<PreviewIssueDto[]> GetAllDoneAsync(CancellationToken cancellationToken = default);
    Task<IssueDto> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken = default);
    Task<IssueDto> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PreviewIssueDto[]> GetAllAsync(CancellationToken cancellationToken);
}