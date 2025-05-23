using Microsoft.EntityFrameworkCore;

namespace Sakila.API.Data
{
    public class SakilaContext : DbContext
    {
        public SakilaContext(DbContextOptions<SakilaContext> options) : base(options)
        {
        }

        // DbSets aquí luego del scaffold
    }
}
