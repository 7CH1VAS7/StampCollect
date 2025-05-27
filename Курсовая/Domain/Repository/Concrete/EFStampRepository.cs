using Microsoft.EntityFrameworkCore;
using Курсовая.Domain.Entity;
using Курсовая.Domain.Repository.Abstract;

namespace Курсовая.Domain.Repository.Concrete
{
    public class EFStampRepository : IStampRepository
    {
        AppDbContext _context;
        public EFStampRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveStampAsync(Stamp stamp)
        {
            _context.Entry(stamp).State = stamp.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Stamp>> GetStampAllAsync()
        {
            var a = await _context.Stamps.Include(a=>a.Collections).ThenInclude(a=>a.Collector).ToListAsync();
            return a;
        }

        public async Task DelStamp(int id)
        {
            _context.Stamps.Entry(new Stamp() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }
        public async Task<Stamp?> GetStampByIdAsync(int id)
        {
            return await _context.Stamps.FirstOrDefaultAsync(x => x.Id == id);

        }
    }
}
