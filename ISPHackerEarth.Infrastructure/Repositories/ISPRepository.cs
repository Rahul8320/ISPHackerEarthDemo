using ISPHackerEarth.Domain.Entities;
using ISPHackerEarth.Domain.Repositories;
using ISPHackerEarth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISPHackerEarth.Infrastructure.Repositories;

internal class ISPRepository(ISPDbContext dbContext) : IISPRepository
{
    public async Task<IEnumerable<ISP>> GetAll(CancellationToken cancellationToken)
    {
        return await dbContext.ISPs.ToListAsync(cancellationToken);
    }

    public async Task<ISP?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.ISPs.AsNoTracking().FirstOrDefaultAsync(isp => isp.Id == id, cancellationToken);
    }

    public async Task<bool> Add(ISP entity, CancellationToken cancellationToken)
    {
        await dbContext.ISPs.AddAsync(entity, cancellationToken);
        return await Complete(cancellationToken);
    }

    public async Task<bool> Update(ISP entity, CancellationToken cancellationToken)
    {
        dbContext.ISPs.Update(entity);
        return await Complete(cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.ISPs.Where(isp => isp.Id == id).ExecuteDeleteAsync(cancellationToken) > 0;
    }

    private async Task<bool> Complete(CancellationToken cancellationToken)
    {
        var result = await dbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
