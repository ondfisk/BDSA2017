using Microsoft.EntityFrameworkCore;

namespace BDSA2017.Lecture05.Entities
{
    public class FuturamaContext : DbContext
    {
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<EpisodeCharacter> EpisodeCharacters { get; set; }

        public FuturamaContext()
        {
        }

        public FuturamaContext(DbContextOptions<FuturamaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Futurama;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EpisodeCharacter>()
                .HasKey(e => new { e.EpisodeId, e.CharacterId });
        }
    }
}
