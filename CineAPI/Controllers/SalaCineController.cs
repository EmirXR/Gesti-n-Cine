using CineAPI.Models;
using CineAPI.Repository;
using CineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaCineController : ControllerBase
    {
        private readonly ICineService _cineService;
        private readonly ICineRepository _repository;

        public SalaCineController(ICineService cineService, ICineRepository repository)
        {
            _cineService = cineService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalas() => Ok(await _repository.ObtenerSalasAsync());

        [HttpGet("estado-disponibilidad")]
        public async Task<IActionResult> ConsultarEstado([FromQuery] string nombreSala)
        {
            var result = await _cineService.ConsultarEstadoSalaAsync(nombreSala);
            return Ok(result);
        }

        [HttpPost("asignar-pelicula")]
        public async Task<IActionResult> AsignarPelicula([FromBody] AsignarPeliculaDto dto)
        {
            await _cineService.AsignarPeliculaAsync(dto);
            return Ok(new { mensaje = "Película asignada correctamente" });
        }

        [HttpGet("dashboard-indicadores")]
        public async Task<IActionResult> GetDashboard()
        {
            return Ok(await _cineService.GetDashboardIndicadoresAsync());
        }

        [HttpGet("asignaciones")]
        public async Task<IActionResult> GetAsignaciones()
        {
            var asignaciones = await _repository.ObtenerAsignacionesAsync();
            return Ok(asignaciones);
        }

        [HttpDelete("asignaciones")]
        public async Task<IActionResult> EliminarAsignacion([FromQuery] int idSalaCine, [FromQuery] int idPelicula)
        {
            await _repository.EliminarAsignacionAsync(idSalaCine, idPelicula);
            return Ok(new { mensaje = "Asignación eliminada correctamente" });
        }
    }
}