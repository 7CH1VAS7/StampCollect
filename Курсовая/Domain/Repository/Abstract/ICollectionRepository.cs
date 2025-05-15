using Курсовая.Domain.Entity;

namespace Курсовая.Domain.Repository.Abstract
{
    public interface ICollectionRepository
    {
        public Task SaveCollectionAsync(Collection collection);
        public Task DeleteCollectionAsync(int id);
        public Task<Collection?> GetCollectionByIdAsync(int id);
        public Task <IEnumerable<Collection>> GetCollectionAllAsync();
    }
}
