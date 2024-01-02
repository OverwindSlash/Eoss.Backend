using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Eoss.Backend.EntityFrameworkCore
{
    public static class BackendDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BackendDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public static void Configure(DbContextOptionsBuilder<BackendDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection, ServerVersion.AutoDetect(connection.ConnectionString));
        }
    }
}
