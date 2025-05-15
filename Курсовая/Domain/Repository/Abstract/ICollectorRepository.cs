using Курсовая.Domain.Entity;

namespace Курсовая.Domain.Repository.Abstract
{
    public interface ICollectorRepository
    {
        public Task SaveCollectorAsync(Collector collector);
        public Task DelCollectorAsync(int id);
        public Task<IEnumerable<Collector>> GetCollectorAllAsync();
        public Task<Collector?> GetCollectorByIdAsync(int id);
    }
}
