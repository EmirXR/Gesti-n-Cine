using CineAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CineAPI.Repository
{
    public class CineRepository : ICineRepository
    {
        private readonly ApplicationDbContext _context;

        public CineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pelicula>> ObtenerPeliculasAsync()
            => await _context.Peliculas.Where(p => p.Activo).ToListAsync();

        public async Task<Pelicula?> ObtenerPeliculaPorIdAsync(int id)
            => await _context.Peliculas.FirstOrDefaultAsync(p => p.IdPelicula == id && p.Activo);

        public async Task<IEnumerable<Pelicula>> BuscarPeliculaPorNombreAsync(string nombre)
            => await _context.Peliculas.Where(p => p.Activo && p.Nombre.Contains(nombre)).ToListAsync();

        public async Task CrearPeliculaAsync(Pelicula pelicula)
        {
            await _context.Peliculas.AddAsync(pelicula);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarPeliculaAsync(Pelicula pelicula)
        {
            _context.Peliculas.Update(pelicula);
            await _context.SaveChangesAsync();
        }

        public async Task EliminacionLogicaPeliculaAsync(int id)
        {
            var asignaciones = _context.PeliculasSalasCine.Where(ps => ps.IdPelicula == id);
            _context.PeliculasSalasCine.RemoveRange(asignaciones);

            var p = await _context.Peliculas.FindAsync(id);
            if (p != null)
            {
                _context.Peliculas.Remove(p); 
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PeliculaSalaCine>> ObtenerPeliculasPorFechaPublicacionAsync(DateTime fecha)
        {
            return await _context.PeliculasSalasCine
                .Include(ps => ps.Pelicula)
                .Include(ps => ps.SalaCine)
                .Where(ps => ps.Activo && ps.FechaPublicacion.Date == fecha.Date)
                .ToListAsync();
        }

        public async Task<RespuestaEstadoSalaDto> ConsultarEstadoSalaSPAsync(string nombreSala)
        {
            var param = new SqlParameter("@NombreSala", nombreSala ?? string.Empty);
            var result = await _context.Database
                .SqlQueryRaw<RespuestaEstadoSalaDto>("EXEC sp_ConsultarEstadoSala @NombreSala", param)
                .ToListAsync();

            return result.FirstOrDefault() ?? new RespuestaEstadoSalaDto { Mensaje = "Sala no encontrada", CantidadPeliculas = 0 };
        }

        public async Task AsignarPeliculaASalaAsync(PeliculaSalaCine asignacion)
        {
            await _context.PeliculasSalasCine.AddAsync(asignacion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaCine>> ObtenerSalasAsync()
            => await _context.SalasCine.Where(s => s.Estado).ToListAsync();

        public async Task<DashboardIndicadoresDto> ObtenerIndicadoresDashboardAsync()
        {
            var totalSalas = await _context.SalasCine.CountAsync(s => s.Estado);
            var totalPeliculas = await _context.Peliculas.CountAsync(p => p.Activo);

            var salas = await _context.SalasCine.Where(s => s.Estado).ToListAsync();
            int salasDisponibles = 0;

            foreach (var sala in salas)
            {
                var count = await _context.PeliculasSalasCine.CountAsync(ps => ps.IdSalaCine == sala.IdSala && ps.Activo);
                if (count < 3) salasDisponibles++;
            }

            return new DashboardIndicadoresDto
            {
                TotalSalas = totalSalas,
                TotalSalasDisponibles = salasDisponibles,
                TotalPeliculas = totalPeliculas
            };
        }

        public async Task<IEnumerable<object>> ObtenerAsignacionesAsync()
        {
            return await _context.PeliculasSalasCine
                .Include(ps => ps.SalaCine)
                .Include(ps => ps.Pelicula)
                .Select(ps => new
                {
                    idSalaCine = ps.IdSalaCine,
                    idPelicula = ps.IdPelicula,
                    nombreSala = ps.SalaCine != null ? ps.SalaCine.Nombre : "",
                    nombrePelicula = ps.Pelicula != null ? ps.Pelicula.Nombre : "",
                    fechaPublicacion = ps.FechaPublicacion,
                    fechaFin = ps.FechaFin
                })
                .ToListAsync();
        }

        public async Task EliminarAsignacionAsync(int idSalaCine, int idPelicula)
        {
            var asignacion = await _context.PeliculasSalasCine.
            FirstOrDefaultAsync(ps => ps.IdSalaCine == idSalaCine && ps.IdPelicula == idPelicula);
            
            if (asignacion != null)
            {
                _context.PeliculasSalasCine.Remove(asignacion);
                await _context.SaveChangesAsync();
            }
        }
    }
}