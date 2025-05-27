using Microsoft.EntityFrameworkCore;
using Курсовая.Domain.Entity;
using Курсовая.Domain.Repository.Abstract;
using Курсовая.Models;

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

        public async Task<List<Collector>> GetCollectorByStampRare()
        {
            var a = await _context.Collectors.Include(a=>a.Collections).ThenInclude(a=> a.Stamps.Where(a=>a.CatalogPrice>1000)).ToListAsync();
            return a;
        }

        public async Task<TransModel> GetRichCollector()
        {
            var a = await _context.Collectors.Include(a => a.Collections).ThenInclude(a => a.Stamps).ToListAsync();
            int Max = 0;
            int idStamp = 0;
            int idCollector = 0;

            foreach (var r in a)
            {foreach(var c in r.Collections)
             {foreach (var t in c.Stamps)
              {if (t.CatalogPrice > Max)
               {
                    Max = (int)t.CatalogPrice;
                    idStamp = t.Id;
                    idCollector = r.Id;
               }
              }
             }
            }
            Collector? RichCollector = await _context.Collectors.FirstOrDefaultAsync(u=>u.Id == idCollector);
            Stamp? MaxStamp = await _context.Stamps.FirstOrDefaultAsync(s=>s.Id == idStamp);

            TransModel Container = new TransModel(RichCollector!, MaxStamp!);
            return Container;
        }

        public async Task<Collector> GetBigRare()
        {
            var a = await _context.Collectors.Include(a => a.Collections).ThenInclude(a => a.Stamps).ToListAsync();
            int Max = 0;
            int idCollector = 0;
            foreach(var r in a)
            {
                if(r.RareStampsCount > Max)
                {
                    Max = r.RareStampsCount;
                    idCollector = r.Id;
                }
            }
            var b = await _context.Collectors.FirstOrDefaultAsync(s=>s.Id == idCollector);
            return b!;
        }
        public async Task<IEnumerable<Collector>> SortColl()
        {
            return await _context.Collectors
                .Select(c => new
                {
                    Collector = c,
                    CollectionValue = c.Collections
                        .SelectMany(col => col.Stamps)
                        .Sum(s => s.CatalogPrice)
                })
                .OrderByDescending(x => x.CollectionValue)
                .Select(x => x.Collector)
                .Include(c => c.Collections)
                    .ThenInclude(col => col.Stamps)
                .ToListAsync();
        }
        public async Task<IEnumerable<Collector>> OldStamps()
        {
            var tenYearsAgo = DateTime.Now.AddYears(-10).Year;

            return await _context.Collectors
                .Include(c => c.Collections)
                    .ThenInclude(col => col.Stamps)
                .Where(c => c.Collections.Any(col =>
                    col.Stamps.Any(s => s.ReleaseYear < tenYearsAgo)))
                .ToListAsync();
        }




    }
}
