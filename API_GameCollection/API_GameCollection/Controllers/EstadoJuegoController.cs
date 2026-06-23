using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_GameCollection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadoJuegoController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;

        public EstadoJuegoController(GameCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region GET

        // GET: /EstadoJuego
        [HttpGet]
        public IActionResult GetAll()
        {
            var estados = _context.EstadoJuegos.ToList();

            var listaDTO = _mapper.Map<List<EstadoJuegoDTO>>(estados);
            return Ok(listaDTO);
        }

        #endregion
    }
}
