namespace CineAPI.Models
{
    public class Pelicula
    {
        public int IdPelicula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Duracion { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class SalaCine
    {
        public int IdSala { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }

    public class PeliculaSalaCine
    {
        public int IdPeliculaSala { get; set; }
        public int IdSalaCine { get; set; }
        public int IdPelicula { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; } = true;

        public Pelicula? Pelicula { get; set; }
        public SalaCine? SalaCine { get; set; }
    }
}