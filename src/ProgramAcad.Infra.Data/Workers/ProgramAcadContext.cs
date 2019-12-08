using Microsoft.EntityFrameworkCore;

namespace ProgramAcad.Infra.Data.Workers
{
    public class ProgramAcadDataContext : DbContext
    {
        public ProgramAcadDataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }
    }
}
