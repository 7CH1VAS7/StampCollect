using Курсовая.Domain.Repository.Abstract;

namespace Курсовая.Domain
{
    public class DataManager
    {
        public IStampRepository stampRepository {get; set;}
        public ICollectorRepository collectorRepository {get; set;}
        public ICollectionRepository collectionRepository {get; set;}
        public DataManager (IStampRepository stampRepository, ICollectorRepository collectorRepository, ICollectionRepository collectionRepository)
        {
            this.stampRepository = stampRepository;
            this.collectorRepository = collectorRepository;
            this.collectionRepository = collectionRepository;
        }
    }
}
