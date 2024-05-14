using ISPHackerEarth.Domain.Entities;
using ISPHackerEarth.Domain.Repositories;

namespace ISPHackerEarth.Infrastructure.Repositories;

internal class ISPRepository : IISPRepository
{
    public Task Add(ISP entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ISP>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ISP?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(ISP entity)
    {
        throw new NotImplementedException();
    }
}
