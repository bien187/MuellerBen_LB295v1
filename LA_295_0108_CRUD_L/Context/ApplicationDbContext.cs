using Microsoft.EntityFrameworkCore;
using LA_295_0108_CRUD_L.Model;

namespace LA_295_0108_CRUD_L.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Film> Filme { get; set; }
        public DbSet<Genre> Genres { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmId);
                // Hier können weitere Konfigurationen für die Film-Entität hinzugefügt werden
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreId);
                // Hier können weitere Konfigurationen für die Genre-Entität hinzugefügt werden
            });
        }
    }
}
