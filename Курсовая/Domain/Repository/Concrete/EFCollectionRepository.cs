using Microsoft.EntityFrameworkCore;
using Курсовая.Domain.Entity;
using Курсовая.Domain.Repository.Abstract;

namespace Курсовая.Domain.Repository.Concrete
{
    public class EFCollectionRepository : ICollectionRepository
    {
        AppDbContext _context;
        public EFCollectionRepository(AppDbContext context) 
        {
            _context = context;
        }
        
        public async Task UpperStamp(Collection collection, List<int> stampIds)
        {
            var UpCollection = _context.Collections.Include(s => s.Stamps).Include(s => s.Collector).FirstOrDefault(s => s.Id == collection.Id);
            UpCollection!.Stamps.Clear();
            var UpStamps = await _context.Stamps.Where(s => stampIds.Contains(s.Id)).ToListAsync();
            UpCollection.Stamps = UpStamps;

            _context.Entry(UpCollection).State = collection.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();

        }



        public async Task SaveCollectionAsync(Collection collection)
        {
            _context.Entry(collection).State = collection.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCollectionAsync(int id)
        {
            _context.Entry(new Collection { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _context.Collections.Include(c => c.Stamps).Include(c => c.Collector).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Collection>> GetCollectionAllAsync()
        {
            return await _context.Collections.Include(c => c.Stamps).Include(c => c.Collector).ToListAsync();
        }
        


    }
}
