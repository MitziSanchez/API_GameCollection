using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_GameCollection.Controllers
{
    [Route("Colecciones/")]
    [ApiController]
    public class DetallesColeccionController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;

        public DetallesColeccionController(GameCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region POST

        // POST: Colecciones/id/Detalles
        [HttpPost("{id}/Detalles")]
        public IActionResult Create(int id, [FromBody] DetalleColeccionCreateDTO nuevo_dc)
        {
            try
            {
                // Verificar modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verifica que exista la colección
                var coleccion = _context.Coleccions.Find(id);
                if(coleccion == null)
                {
                    return Conflict(new
                    {
                        message = "La colección no existe."
                    });
                }

                // Verificar que el juego a insertar no exista en la colección
                var juegoExistente = _context.DetalleColeccions
                    .Where(j => j.ColeccionId == id && j.VideojuegoId == nuevo_dc.VideojuegoId)
                    .FirstOrDefault();

                if(juegoExistente != null)
                {
                    return Conflict(new
                    {
                        message = "El videojuego ya existe en la colección."
                    });
                }

                // Crear detalle en la colección, Estado por defecto Adquirido 1
                var insertarDetalle = _mapper.Map<DetalleColeccion>(nuevo_dc);
                insertarDetalle.EstadoJuegoId = 1; 

                _context.DetalleColeccions.Add(insertarDetalle);
                _context.SaveChanges();

                // Recuperar detalle insertado
                var buscarDetalle = _context.DetalleColeccions
                    .Where(d => d.DetalleColeccionId == insertarDetalle.DetalleColeccionId)
                    .Include(d => d.Videojuego)
                        .ThenInclude(v => v.Genero)
                    .Include(d => d.Videojuego).
                        ThenInclude(v => v.Plataforma)
                    .Include(d => d.EstadoJuego)
                    .FirstOrDefault();

                var mostrarDetalle = _mapper.Map<DetalleColeccionDTO>(buscarDetalle);

                // Devuelve 201 Created + Location header apuntando al GET específico
                return CreatedAtAction(nameof(Get), new { id = insertarDetalle.DetalleColeccionId }, mostrarDetalle);


            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al crear el detalle de colección.",
                    detail = ex.Message
                });
            }
        }

        #endregion

        #region GET

        // GET: Colecciones/Detalles/id
        [HttpGet("Detalles/{id}")]
        public IActionResult Get(int id)
        {
            // Verficar que existe el detalle de coleccion
            var detalle = _context.DetalleColeccions
                .Where(d => d.DetalleColeccionId == id)
                .Include(d => d.Videojuego)
                    .ThenInclude(v => v.Genero)
                .Include(d => d.Videojuego).    
                    ThenInclude(v => v.Plataforma)
                .Include(d => d.EstadoJuego)
                .FirstOrDefault();

            if (detalle == null)
            {
                return NotFound(new
                {
                    message = "El detalle de colección no fue encontrado."
                });
            }

            //Devolver detalle de coleccion
            var mostrarDetalle = _mapper.Map<DetalleColeccionDTO>(detalle);

            return Ok(mostrarDetalle);
        }

        #endregion

        #region PUT

        // PUT: Colecciones/Detalles/id

        #endregion

        #region DELETE

        #endregion 
    }
}
