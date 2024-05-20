using ISPHackerEarth.Domain.Common.Service;
using ISPHackerEarth.Domain.Entities;
using ISPHackerEarth.Domain.Repositories;
using ISPHackerEarth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ISPHackerEarth.Infrastructure.Repositories;

internal class ISPRepository(ISPDbContext dbContext, ILoggerService logger) : IISPRepository
{
    public async Task<IEnumerable<ISP>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext.ISPs.Where(isp => isp.Status == 0).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public async Task<ISP?> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext.ISPs.AsNoTracking().FirstOrDefaultAsync(isp => isp.Id == id && isp.Status == 0, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public async Task<bool> Add(ISP entity, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.ISPs.AddAsync(entity, cancellationToken);
            return await Complete(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public async Task<bool> Update(ISP entity, CancellationToken cancellationToken)
    {
        try
        {
            dbContext.ISPs.Update(entity);
            return await Complete(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    private async Task<bool> Complete(CancellationToken cancellationToken)
    {
        try
        {
            var result = await dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
