using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Курсовая.Domain.Entity;

namespace Курсовая.Domain.Repository.Abstract
{
    public interface IStampRepository
    {
        public  Task SaveStampAsync(Stamp stamp);
        public  Task<IEnumerable<Stamp>> GetStampAllAsync();
        public Task DelStamp(int id);
        public Task<Stamp?> GetStampByIdAsync(int id);
    }
}
