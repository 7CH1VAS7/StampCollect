using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Курсовая.CoreLogin.LogModel;
using Курсовая.Domain;

namespace Курсовая.CoreLogin
{
    public class Core
    {
        AppDbContext _db;
        public IdentityBuilder _identity;
        public Core(AppDbContext db, IdentityBuilder identity) 
        {
            _db = db;
            _identity = identity;
        }

        /*public async Task<IAsyncResult> NewAdmin(AdminModel model)
        {
            
        }*/

    }
}
