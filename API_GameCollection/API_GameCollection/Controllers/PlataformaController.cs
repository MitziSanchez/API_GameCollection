using API_GameCollection.DTOs;
using API_GameCollection.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_GameCollection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlataformaController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;

        public PlataformaController(GameCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region GET

        // GET: /Plataforma
        [HttpGet]
        public IActionResult GetAll()
        {
            var plataformas = _context.Plataformas.ToList();

            var listaDTO = _mapper.Map<List<PlataformaDTO>>(plataformas);
            return Ok(listaDTO);
        }

        #endregion

    }
}
