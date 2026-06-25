using API_GameCollection.DTOs;
using API_GameCollection.Models;
using API_GameCollection.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API_GameCollection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GameCollectionContext _context;
        private readonly IMapper _mapper;
        private readonly PasswordService _Password;

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
            var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound(new
                {
                    message = $"No se encontro el usuario id {id}"
                });
            }

            var mostrarUsuario = _mapper.Map<UsuarioDTO>(usuario);

            return Ok(mostrarUsuario);
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
                    return Conflict(new
                    {
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

        // POST: /Usuario/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Buscar usuario
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == login.Correo);

                if (usuario == null)
                {
                    return NotFound(new
                    {
                        message = "El usuario no se encuentra registrado."
                    });
                }

                // Comprobar contraseñas
                bool acceso = _Password.VerifyPassword(usuario.Contrasena, login.Contrasena);
                if (!acceso)
                {
                    return Unauthorized(new
                    {
                        message = "Contraseña incorrecta."
                    });
                }

                // Contraseña correcta, permite acceso
                // Mapear a DTO de sesion
                var sesion = _mapper.Map<UsuarioSesionDTO>(usuario);

                // Asignar token
                sesion.Token = ("TOKEN_TEMPORAL_DESARROLLO");

                return Ok(sesion);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al iniciar sesión.",
                    detail = ex.Message
                });
            }
        }

        #endregion

        #region PUT

        [HttpPut("{id}/password")]
        public IActionResult UpdatePassword(int id, [FromBody] UsuarioUpdatePassDTO upd)
        {
            try
            {
                // Verifica modelo 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Recupera usuario actual
                var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

                if (usuario == null)
                {
                    return NotFound(new
                    {
                        message = $"No se encontro el usuario id {id}."
                    });
                }

                //Verifica que contraseña actual sea correcta
                bool permiso = _Password.VerifyPassword(usuario.Contrasena, upd.ContrasenaActual);
                if (!permiso)
                {
                    return Unauthorized(new
                    {
                        message = "La contraseña actual no es correcta."
                    });
                }

                //Verificar que las contraseñas no sean iguales               
                if (upd.ContrasenaActual == upd.ContrasenaNueva)
                {
                    return BadRequest(new
                    {
                        message = "La nueva contraseña no puede ser igual a la actual."
                    });
                }
                                             
                // Constraseña correcta, modifica contraseña actual
                usuario.Contrasena = _Password.HashPassword(upd.ContrasenaNueva);
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Contraseña modificada correctamente."
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno al modificar contraseña.",
                    detail = ex.Message
                });
            }
        }

        #endregion
    }
}
