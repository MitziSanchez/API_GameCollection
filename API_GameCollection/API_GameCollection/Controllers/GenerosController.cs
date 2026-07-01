using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_GameCollection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;

        public GenerosController(GameCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region GET

        // GET: /Generos
        [HttpGet]
        public IActionResult GetAll()
        {
            var generos = _context.Generos.ToList();

            var listaDTO = _mapper.Map<List<GeneroDTO>>(generos);
            return Ok(listaDTO);
        }

        #endregion
    }
}
