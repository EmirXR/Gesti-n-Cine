using CineAPI.Models;

namespace CineAPI.Repository
{
    public interface ICineRepository
    {
        Task<IEnumerable<Pelicula>> ObtenerPeliculasAsync();
        Task<Pelicula?> ObtenerPeliculaPorIdAsync(int id);
        Task<IEnumerable<Pelicula>> BuscarPeliculaPorNombreAsync(string nombre);
        Task CrearPeliculaAsync(Pelicula pelicula);
        Task ActualizarPeliculaAsync(Pelicula pelicula);
        Task EliminacionLogicaPeliculaAsync(int id);

        Task<IEnumerable<PeliculaSalaCine>> ObtenerPeliculasPorFechaPublicacionAsync(DateTime fecha);
        Task<RespuestaEstadoSalaDto> ConsultarEstadoSalaSPAsync(string nombreSala);
        Task AsignarPeliculaASalaAsync(PeliculaSalaCine asignacion);
        
        Task<IEnumerable<SalaCine>> ObtenerSalasAsync();
        Task<DashboardIndicadoresDto> ObtenerIndicadoresDashboardAsync();

        Task<IEnumerable<object>> ObtenerAsignacionesAsync();
        Task EliminarAsignacionAsync(int idSalaCine, int idPelicula);
    }
}