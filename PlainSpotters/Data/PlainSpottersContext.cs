using Microsoft.EntityFrameworkCore;
using PlainSpotters.ViewModels;

namespace PlainSpotters.Data
{
    public class PlainSpottersContext : DbContext
    {

        public PlainSpottersContext(DbContextOptions<PlainSpottersContext> options)
            : base(options)
        {
        }

        #region Properties

        public DbSet<Sighting> Sighting { get; set; }

        #endregion
    }
}