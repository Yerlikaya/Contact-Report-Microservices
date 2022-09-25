using Microsoft.EntityFrameworkCore;

namespace Report.Service.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Report.Service.Models.Report> Reports { get; set;}

    }
}
