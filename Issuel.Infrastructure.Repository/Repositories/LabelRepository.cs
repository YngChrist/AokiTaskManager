using Issuel.Application.Common.Interfaces;
using Issuel.Domain.Entities;
using Issuel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Issuel.Infrastructure.Repository.Repositories;

public class LabelRepository : BaseRepository<Label, IssueDbContext>, ILabelRepository
{
    public LabelRepository(IssueDbContext context) : base(context)
    {
    }

    public async Task<Label[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Labels.ToArrayAsync(cancellationToken);
    }
}