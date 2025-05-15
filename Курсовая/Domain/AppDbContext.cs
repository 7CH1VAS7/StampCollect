using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Курсовая.Domain.Entity;



namespace Курсовая.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet <Stamp> Stamps {  get; set; }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<Collection> Collections { get; set; }

    }
}
