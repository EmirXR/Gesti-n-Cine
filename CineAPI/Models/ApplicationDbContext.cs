using Microsoft.EntityFrameworkCore;

namespace CineAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<SalaCine> SalasCine { get; set; }
        public DbSet<PeliculaSalaCine> PeliculasSalasCine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pelicula>().ToTable("pelicula").HasKey(p => p.IdPelicula);
            modelBuilder.Entity<Pelicula>().Property(p => p.IdPelicula).HasColumnName("id_pelicula");
            modelBuilder.Entity<Pelicula>().Property(p => p.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Pelicula>().Property(p => p.Duracion).HasColumnName("duracion");
            modelBuilder.Entity<Pelicula>().Property(p => p.Activo).HasColumnName("activo");

            modelBuilder.Entity<SalaCine>().ToTable("sala_cine").HasKey(s => s.IdSala);
            modelBuilder.Entity<SalaCine>().Property(s => s.IdSala).HasColumnName("id_sala");
            modelBuilder.Entity<SalaCine>().Property(s => s.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<SalaCine>().Property(s => s.Estado).HasColumnName("estado");

            modelBuilder.Entity<PeliculaSalaCine>().ToTable("pelicula_salacine").HasKey(ps => ps.IdPeliculaSala);
            modelBuilder.Entity<PeliculaSalaCine>().Property(ps => ps.IdPeliculaSala).HasColumnName("id_pelicula_sala");
            modelBuilder.Entity<PeliculaSalaCine>().Property(ps => ps.IdSalaCine).HasColumnName("id_sala_cine");
            modelBuilder.Entity<PeliculaSalaCine>().Property(ps => ps.IdPelicula).HasColumnName("id_pelicula");
            modelBuilder.Entity<PeliculaSalaCine>().Property(ps => ps.FechaPublicacion).HasColumnName("fecha_publicacion");
            modelBuilder.Entity<PeliculaSalaCine>().Property(ps => ps.FechaFin).HasColumnName("fecha_fin");
            modelBuilder.Entity<PeliculaSalaCine>().Property(ps => ps.Activo).HasColumnName("activo");

            modelBuilder.Entity<PeliculaSalaCine>()
                .HasOne(ps => ps.SalaCine)
                .WithMany()
                .HasForeignKey(ps => ps.IdSalaCine);

            modelBuilder.Entity<PeliculaSalaCine>()
                .HasOne(ps => ps.Pelicula)
                .WithMany()
                .HasForeignKey(ps => ps.IdPelicula);
        }
    }
}