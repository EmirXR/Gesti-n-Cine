namespace CineAPI.Models
{
    public class PeliculaDto
    {
        public int IdPelicula { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Duracion { get; set; }
    }

    public class AsignarPeliculaDto
    {
        public int IdSalaCine { get; set; }
        public int IdPelicula { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class RespuestaEstadoSalaDto
    {
        public string Mensaje { get; set; } = string.Empty;
        public int CantidadPeliculas { get; set; }
    }

    public class DashboardIndicadoresDto
    {
        public int TotalSalas { get; set; }
        public int TotalSalasDisponibles { get; set; }
        public int TotalPeliculas { get; set; }
    }
}