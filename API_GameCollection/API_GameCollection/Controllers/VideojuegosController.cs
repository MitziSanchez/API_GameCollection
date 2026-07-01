using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace API_GameCollection.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VideojuegosController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;

        public VideojuegosController(GameCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region GET

        // GET: /Videojuegos
        [HttpGet]
        public IActionResult GetAll()
        {
            var videojuegos = _context.Videojuegos
                .Include(v => v.Genero)
                .Include(v => v.Plataforma)
                .ToList();

            var listaDTO = _mapper.Map<List<VideojuegoDTO>>(videojuegos);
            return Ok(listaDTO);
        }

        // GET: /Videojuegos/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var videojuego = _context.Videojuegos
                .Include(v => v.Genero)
                .Include(v => v.Plataforma)
                .FirstOrDefault(v => v.VideojuegoId == id);

            // Si es null, devuvle un NotFound 
            if (videojuego == null)
            {
                return NotFound(new
                {
                    message = $"No se encontro el videojuego id {id}"
                });
            }

            var videojuegoDTO = _mapper.Map<VideojuegoDTO>(videojuego);
            return Ok(videojuegoDTO);
        }

        #endregion

        #region POST

        // POST: /Videojuegos/
        [HttpPost]
        public IActionResult Create([FromBody] VideojuegoCreateDTO nuevoVideojuego)
        {
            try
            {
                // Validar el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Mapear DTO a entidad
                var guardarVideojuego = _mapper.Map<Videojuego>(nuevoVideojuego);

                // Añadir a bd
                _context.Videojuegos.Add(guardarVideojuego);
                _context.SaveChanges();

                // Recuperar videojuego guardado
                var videojuegoGuardado = _context.Videojuegos
                    .Include(v => v.Genero)
                    .Include(v => v.Plataforma)
                    .FirstOrDefault(v => v.VideojuegoId == guardarVideojuego.VideojuegoId);

                // Mapear entidad guardada a DTO de salida
                var mostrarVideojuego = _mapper.Map<VideojuegoDTO>(videojuegoGuardado);

                // Devuelve 201 Created + Location header apuntando al GET específico
                return CreatedAtAction(nameof(Get), new { id = guardarVideojuego.VideojuegoId }, mostrarVideojuego);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    message = "Error interno al crear el videojuego.", 
                    detail = ex.Message 
                });
            }
        }


        #endregion

    }
}
