using ISPHackerEarth.Domain.Entities;

namespace ISPHackerEarth.Domain.Repositories;

public interface IISPRepository
{
    Task<IEnumerable<ISP>> GetAll();
    Task<ISP?> GetById(Guid id);
    Task Add(ISP entity);
    Task Update(ISP entity);
    Task Delete(Guid id);
}
