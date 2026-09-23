using CineAPI.Models;
using CineAPI.Repository;

namespace CineAPI.Services
{
    public class CineService : ICineService
    {
        private readonly ICineRepository _repository;

        public CineService(ICineRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PeliculaDto>> GetPeliculasAsync()
        {
            var items = await _repository.ObtenerPeliculasAsync();
            return items.Select(p => new PeliculaDto { IdPelicula = p.IdPelicula, Nombre = p.Nombre, Duracion = p.Duracion });
        }

        public async Task<IEnumerable<PeliculaDto>> BuscarPeliculasPorNombreAsync(string nombre)
        {
            var items = await _repository.BuscarPeliculaPorNombreAsync(nombre);
            return items.Select(p => new PeliculaDto { IdPelicula = p.IdPelicula, Nombre = p.Nombre, Duracion = p.Duracion });
        }

        public async Task CrearPeliculaAsync(PeliculaDto dto)
        {
            var entity = new Pelicula { Nombre = dto.Nombre, Duracion = dto.Duracion, Activo = true };
            await _repository.CrearPeliculaAsync(entity);
        }

        public async Task ActualizarPeliculaAsync(int id, PeliculaDto dto)
        {
            var p = await _repository.ObtenerPeliculaPorIdAsync(id);
            if (p == null) throw new Exception("Película no encontrada.");

            p.Nombre = dto.Nombre;
            p.Duracion = dto.Duracion;
            await _repository.ActualizarPeliculaAsync(p);
        }

        public async Task EliminarPeliculaLogicoAsync(int id)
        {
            await _repository.EliminacionLogicaPeliculaAsync(id);
        }

        public async Task<IEnumerable<object>> GetPeliculasPorFechaPublicacionAsync(string fechaStr)
        {
            if (!DateTime.TryParse(fechaStr, out DateTime fechaValida))
            {
                throw new ArgumentException("La fecha ingresada no es válida. Use formato YYYY-MM-DD.");
            }

            var asignaciones = await _repository.ObtenerPeliculasPorFechaPublicacionAsync(fechaValida);
            return asignaciones.Select(a => new
            {
                a.IdPeliculaSala,
                Pelicula = a.Pelicula?.Nombre,
                Sala = a.SalaCine?.Nombre,
                a.FechaPublicacion,
                a.FechaFin
            });
        }

        public async Task<RespuestaEstadoSalaDto> ConsultarEstadoSalaAsync(string nombreSala)
        {
            if (string.IsNullOrWhiteSpace(nombreSala))
                throw new ArgumentException("El nombre de la sala es requerido.");

            return await _repository.ConsultarEstadoSalaSPAsync(nombreSala);
        }

        public async Task AsignarPeliculaAsync(AsignarPeliculaDto dto)
        {
            if (dto.FechaFin <= dto.FechaPublicacion)
                throw new ArgumentException("La fecha fin debe ser posterior a la fecha de publicación.");

            var entidad = new PeliculaSalaCine
            {
                IdSalaCine = dto.IdSalaCine,
                IdPelicula = dto.IdPelicula,
                FechaPublicacion = dto.FechaPublicacion,
                FechaFin = dto.FechaFin,
                Activo = true
            };

            await _repository.AsignarPeliculaASalaAsync(entidad);
        }

        public async Task<DashboardIndicadoresDto> GetDashboardIndicadoresAsync()
        {
            return await _repository.ObtenerIndicadoresDashboardAsync();
        }
    }
}