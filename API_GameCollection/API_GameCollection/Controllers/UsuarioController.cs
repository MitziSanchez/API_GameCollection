using API_GameCollection.DTOs;
using API_GameCollection.Models;
using API_GameCollection.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_GameCollection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _Password = new PasswordService();

        public UsuarioController(GameCollectionContext context, IMapper mapper, PasswordService password)
        {
            _context = context;
            _mapper = mapper;
            _Password = password;
        }

        #region GET

        // GET: /Usuario
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            return Ok();
        }

        #endregion

        #region POST

        // POST : /Usuario
        [HttpPost]
        public IActionResult Create([FromBody] UsuarioCreateDTO nuevoUsuario)
        {
            try
            {
                // Validar el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validar si el nombre de usuario ya existe
                if (_context.Usuarios.Any(u => u.Nombre == nuevoUsuario.Nombre))
                {
                    // Http 409, el recurso no puede crearse por que ya existe
                    return Conflict(new {
                        message = "El nombre de usuario ya está registrado."
                    });
                }

                // Validar si el correo ya existe
                if (_context.Usuarios.Any(u => u.Correo == nuevoUsuario.Correo))
                {
                    // Http 409, el recurso no puede crearse por que ya existe
                    return Conflict(new
                    {
                        message = "El correo ya está registrado."
                    });
                }                

                // Mapear DTO a entidad
                var guardarUsuario = _mapper.Map<Usuario>(nuevoUsuario);

                // Encriptar contraseña              
                guardarUsuario.Contrasena = _Password.HashPassword(guardarUsuario.Contrasena);

                // Añadir a bd
                _context.Usuarios.Add(guardarUsuario);
                _context.SaveChanges();

                // Recuperar usuario guardado
                var usuarioGuardado = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == guardarUsuario.UsuarioId);

                // Mapear entidad guardada a DTO de salida
                var mostrarUsuario = _mapper.Map<UsuarioDTO>(usuarioGuardado);

                // Devuelve 201 Created + Location header apuntando al GET específico
                return CreatedAtAction(nameof(Get), new { id = guardarUsuario.UsuarioId }, mostrarUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al crear el usuario",
                    detail = ex.Message
                });
            }
        }

        #endregion
    }
}
