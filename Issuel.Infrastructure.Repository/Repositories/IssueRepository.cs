using System.Linq.Expressions;
using Issuel.Application.Common.Interfaces;
using Issuel.Domain.Entities;
using Issuel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Issuel.Infrastructure.Repository.Repositories;

public class IssueRepository : BaseRepository<Issue, IssueDbContext>, IIssueRepository
{
    public IssueRepository(IssueDbContext context) : base(context)
    {
    }

    public Task<Issue[]> SearchAsync(Expression<Func<Issue, bool>> filter, CancellationToken cancellationToken = default)
    {
        return Context.Issues.Where(filter).ToArrayAsync(cancellationToken);
    }
}