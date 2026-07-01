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
                if (coleccion == null)
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

                if (juegoExistente != null)
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
        [HttpPut("Detalles/{id}")]
        public IActionResult Update(int id, [FromBody] DetalleColeccionUpdateDTO upd)
        {
            try
            {

                // Verificar modelo 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar que exista el detalle de colección
                var detColeccion = _context.DetalleColeccions.FirstOrDefault(c => c.DetalleColeccionId == id);

                if (detColeccion == null)
                {
                    return NotFound(new
                    {
                        message = "El detalle de colección no existe."
                    });
                }

                // Modificar registro de detalle de colección                
                if (upd.Calificacion != null)
                {
                    // Verifica que calificacion sea de 0 a 10, decimal
                    if (upd.Calificacion >= 0 && upd.Calificacion <= 10)
                    {
                        // Verifica que existan cambios en la calificación
                        if (upd.Calificacion != detColeccion.Calificacion)
                        {
                            detColeccion.Calificacion = upd.Calificacion;
                            detColeccion.FechaCalificacion = DateTime.Now;
                        }
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            message = "La calificación debe tener valores entre 0 y 10."
                        });
                    }                    
                }

                detColeccion.EstadoJuegoId = upd.EstadoJuegoId;
                
                _context.SaveChanges();

                // Recuperar detalle de colección actualizado
                var detalleActualizado = _context.DetalleColeccions
                    .Include(d => d.Videojuego)
                        .ThenInclude(v => v.Genero)
                    .Include(d => d.Videojuego)
                        .ThenInclude(v => v.Plataforma)
                    .Include(d => d.EstadoJuego)
                    .FirstOrDefault(d => d.DetalleColeccionId == id);

                // Mapear a dto
                var mostrarDetalle = _mapper.Map<DetalleColeccionDTO>(detalleActualizado);

                return Ok(mostrarDetalle);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al modificar el detalle de colección.",
                    detail = ex.Message
                });
            }

        }

        #endregion

        #region DELETE

        // DELETE: Colecciones/Detalles/id
        [HttpDelete("Detalles/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {                

                // Verificar que exista el detalle de colección
                var detColeccion = _context.DetalleColeccions.FirstOrDefault(c => c.DetalleColeccionId == id);

                if (detColeccion == null)
                {
                    return NotFound(new
                    {
                        message = "El detalle de colección no existe."
                    });
                }

                // Eliminar
                _context.DetalleColeccions.Remove(detColeccion);
                _context.SaveChanges();

                //return Ok(new
                //{
                //    message = "Detalle de colección eliminado correctamente."
                //});

                // Devuelve 204 No Content al eliminar correctamente
                return NoContent(); 

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al eliminar el detalle de colección.",
                    detail = ex.Message
                });
            }
        }

        #endregion 
    }
}
