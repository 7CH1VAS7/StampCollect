using Microsoft.EntityFrameworkCore;
using Курсовая.Domain.Entity;
using Курсовая.Domain.Repository.Abstract;

namespace Курсовая.Domain.Repository.Concrete
{
    public class EFCollectorRepository : ICollectorRepository
    {
        AppDbContext _context;
        public EFCollectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveCollectorAsync(Collector collector) 
        {
            _context.Entry(collector).State = collector.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DelCollectorAsync(int id)
        {
            _context.Entry(new Collector { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Collector>> GetCollectorAllAsync()
        {
            var collectors = await _context.Collectors.Include(c => c.Collections).ThenInclude(col => col.Stamps.Where(s => s.CatalogPrice > 1000)).ToListAsync();

            return collectors;

        }

        public async Task<Collector?> GetCollectorByIdAsync(int id)
        {
            return await _context.Collectors.FirstOrDefaultAsync(x => x.Id == id);
        }
        

        
    }
}
