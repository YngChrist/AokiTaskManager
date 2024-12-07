using Ardalis.GuardClauses;
using Issuel.Application.Common.Dto.Issue;
using Issuel.Application.Common.Dto.Issue.Requests;
using Issuel.Application.Common.Interfaces;
using Issuel.Application.Common.Mappings;
using Issuel.Domain;
using Issuel.Domain.Entities;
using Issuel.Domain.Validation;

namespace Issuel.Application.Common.Services;

public class IssueService
{
    private readonly IIssueRepository _issueRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IssueMapper _issueMapper;
    
    public IssueService(IIssueRepository issueRepository, IssueMapper issueMapper, IUnitOfWork unitOfWork)
    {
        _issueRepository = issueRepository;
        _issueMapper = issueMapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IssueDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id, nameof(id));
        
        var issue = await _issueRepository.GetByIdAsync(id, cancellationToken);
        ThrowIf.EntityIsNull(issue, id, nameof(Issue));
        
        return _issueMapper.Map(issue!);
    }

    public async Task<IssueDto[]> GetAllUndoneAsync(CancellationToken cancellationToken = default)
    {
        var issues = await _issueRepository.SearchAsync(x => x.Status != Status.Done, cancellationToken);

        return _issueMapper.Map(issues);
    }
    
    public async Task<IssueDto[]> GetAllDoneAsync(CancellationToken cancellationToken = default)
    {
        var issues = await _issueRepository.SearchAsync(x => x.Status == Status.Done, cancellationToken);

        return _issueMapper.Map(issues);
    }

    public async Task<IssueDto> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(CreateIssueRequest));
        
        var issue = new Issue(request.Title, request.Priority, request.Description, request.Deadline, request.Estimate);
        request.Labels.ForEach(x => issue.SaveLabel(new Label(x.Name)));
        
        await _issueRepository.AddAsync(issue, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _issueMapper.Map(issue);
    }

    public async Task<IssueDto> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(UpdateIssueRequest));
        
        var issue = await _issueRepository.GetByIdAsync(request.Id, cancellationToken);
        ThrowIf.EntityIsNull(issue, request.Id, nameof(Issue));
        
        issue!.Update(request.Title, request.Priority, request.Status, request.Description, request.Deadline, request.Estimate);
        
        var labelsForDelete = issue.Labels
            .Where(x => request.Labels.All(y => y.Id != x.Id))
            .Select(x => x.Id)
            .ToArray();

        foreach (var label in labelsForDelete)
        {
            issue.DeleteLabel(label);
        }

        foreach (var label in request.Labels)
        {
            issue.SaveLabel(new Label(label.Name));
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _issueMapper.Map(issue);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id, nameof(id));
        
        var issue = await _issueRepository.GetByIdAsync(id, cancellationToken);
        ThrowIf.EntityIsNull(issue, id, nameof(Issue));
        
        await _issueRepository.DeleteAsync(issue!, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}