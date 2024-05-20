using ISPHackerEarth.Domain.Entities;

namespace ISPHackerEarth.Domain.Repositories;

public interface IISPRepository
{
    Task<IEnumerable<ISP>> GetAll(CancellationToken cancellationToken);
    Task<ISP?> GetById(Guid id, CancellationToken cancellationToken);
    Task<bool> Add(ISP entity, CancellationToken cancellationToken);
    Task<bool> Update(ISP entity, CancellationToken cancellationToken);
}
