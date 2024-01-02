using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Eoss.Backend.Authorization.Roles;
using Eoss.Backend.Authorization.Users;
using Eoss.Backend.MultiTenancy;

namespace Eoss.Backend.EntityFrameworkCore
{
    public class BackendDbContext : AbpZeroDbContext<Tenant, Role, User, BackendDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public BackendDbContext(DbContextOptions<BackendDbContext> options)
            : base(options)
        {
        }
    }
}
