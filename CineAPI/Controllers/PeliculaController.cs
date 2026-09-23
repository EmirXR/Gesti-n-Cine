using CineAPI.Models;
using CineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculaController : ControllerBase
    {
        private readonly ICineService _service;

        public PeliculaController(ICineService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetPeliculasAsync());

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNombre([FromQuery] string nombre)
            => Ok(await _service.BuscarPeliculasPorNombreAsync(nombre));

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PeliculaDto dto)
        {
            await _service.CrearPeliculaAsync(dto);
            return Ok(new { mensaje = "Película creada exitosamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] PeliculaDto dto)
        {
            await _service.ActualizarPeliculaAsync(id, dto);
            return Ok(new { mensaje = "Película actualizada exitosamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarPeliculaLogicoAsync(id);
            return Ok(new { mensaje = "Película eliminada correctamente" });
        }

        [HttpGet("por-fecha")]
        public async Task<IActionResult> ObtenerPorFecha([FromQuery] string fecha)
        {
            try
            {
                var result = await _service.GetPeliculasPorFechaPublicacionAsync(fecha);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
