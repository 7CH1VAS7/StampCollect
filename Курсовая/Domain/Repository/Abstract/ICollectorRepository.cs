using Курсовая.Domain.Entity;
using Курсовая.Models;

namespace Курсовая.Domain.Repository.Abstract
{
    public interface ICollectorRepository
    {
        public Task SaveCollectorAsync(Collector collector);
        public Task DelCollectorAsync(int id);
        public Task<IEnumerable<Collector>> GetCollectorAllAsync();
        public Task<Collector?> GetCollectorByIdAsync(int id);
        public Task<List<Collector>> GetCollectorByStampRare();
        public Task<TransModel> GetRichCollector();
        public Task<Collector> GetBigRare();
        public Task<IEnumerable<Collector>> SortColl();
        public Task<IEnumerable<Collector>> OldStamps();
    }
}
