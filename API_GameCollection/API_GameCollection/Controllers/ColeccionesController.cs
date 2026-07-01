using API_GameCollection.DTOs;
using API_GameCollection.Models;
using API_GameCollection.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_GameCollection.Controllers
{
    [Route("Usuarios/")]
    [ApiController]
    public class ColeccionesController : ControllerBase
    {

        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;

        public ColeccionesController(GameCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region POST

        // POST: usuarios/id/Colecciones
        [HttpPost("{id}/Colecciones")]
        public IActionResult Create(int id)
        {
            // Verificar si usuario existe
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound(new
                {
                    message = "El usuario no fue encontrado."
                });
            }

            // Verifica si el usuario tiene creada una coleccion
            // Por modelo el usuario puede tener una unica colección
            bool existe = _context.Coleccions.Any(c => c.UsuarioId == id);
            if (existe)
            {
                return Conflict(new
                {
                    message = "El usuario ya posee una colección."
                });
            }

            // Crear coleccion
            var coleccion = new Coleccion();
            coleccion.UsuarioId = id;

            _context.Coleccions.Add(coleccion);
            _context.SaveChanges();

            // Recuperar colección
            var recuperarColeccion = _context.Coleccions.FirstOrDefault(c => c.ColeccionId == coleccion.ColeccionId);
            var mostrarColeccion = _mapper.Map<ColeccionDTO>(recuperarColeccion);

            return CreatedAtAction(nameof(Get), new { id = coleccion.ColeccionId }, mostrarColeccion);

        }


        #endregion

        #region GET

        // GET: Usuarios/Colecciones/id
        [HttpGet("Colecciones/{id}")]
        public IActionResult Get(int id)
        {
            // Verificar si la colección existe
            var buscarColeccion = _context.Coleccions.FirstOrDefault(c => c.ColeccionId == id);
            if (buscarColeccion == null)
            {
                return NotFound(new
                {
                    message = "La colección no existe."
                });
            }

            // Recuperar la colección completa y sus detalles
            var coleccion = _context.Coleccions
                .Include(c => c.DetalleColeccions)
                    .ThenInclude(d => d.Videojuego)
                        .ThenInclude(v => v.Genero)
                .Include(c => c.DetalleColeccions)
                    .ThenInclude(d => d.Videojuego)
                        .ThenInclude(v => v.Plataforma)
                .Include(c => c.DetalleColeccions)
                    .ThenInclude(d => d.EstadoJuego)
                .FirstOrDefault(c => c.ColeccionId == id);

            // Mapear a dto
            var mostrarColeccion = _mapper.Map<ColeccionCompletaDTO>(coleccion);

            return Ok(mostrarColeccion);

        }

        #endregion
    }
}
