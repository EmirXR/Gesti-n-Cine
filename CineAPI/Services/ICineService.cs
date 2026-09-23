using CineAPI.Models;

namespace CineAPI.Services
{
    public interface ICineService
    {
        Task<IEnumerable<PeliculaDto>> GetPeliculasAsync();
        Task<IEnumerable<PeliculaDto>> BuscarPeliculasPorNombreAsync(string nombre);
        Task CrearPeliculaAsync(PeliculaDto dto);
        Task ActualizarPeliculaAsync(int id, PeliculaDto dto);
        Task EliminarPeliculaLogicoAsync(int id);
        
        Task<IEnumerable<object>> GetPeliculasPorFechaPublicacionAsync(string fechaStr);
        Task<RespuestaEstadoSalaDto> ConsultarEstadoSalaAsync(string nombreSala);
        Task AsignarPeliculaAsync(AsignarPeliculaDto dto);
        Task<DashboardIndicadoresDto> GetDashboardIndicadoresAsync();
    }
}