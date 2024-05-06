using Abp.Zero.EntityFrameworkCore;
using Eoss.Backend.Authorization.Roles;
using Eoss.Backend.Authorization.Users;
using Eoss.Backend.Entities;
using Eoss.Backend.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Eoss.Backend.EntityFrameworkCore
{
    public class BackendDbContext : AbpZeroDbContext<Tenant, Role, User, BackendDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Device> Devices { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<InstallationParams> InstallationParams { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<PtzParams> PtzParams { get; set; }
        

        public BackendDbContext(DbContextOptions<BackendDbContext> options)
            : base(options)
        {
        }
    }
}
