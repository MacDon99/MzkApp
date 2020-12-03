using Microsoft.EntityFrameworkCore;

namespace Mzk.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Station> Stations { get; set; }
        public DbSet<TimesOfArrivals> TimesOfArrivalsWithPeriods { get; set; }
    }
}