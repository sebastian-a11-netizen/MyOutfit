using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrarse([FromForm] User user)
        {
            await userRepository.CrearUsuario(user);
            return Ok(new { success = true, message = "Usuario creado exitosamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> IniciarSesion([FromForm] string email, [FromForm] string password)
        {
            var user = await userRepository.ObtenerUsuarioPorEmail(email);

            if (user == null || user.Password != password)
                return Unauthorized(new { success = false, message = "Credenciales inválidas" });

            return Ok(new { success = true, message = "Inicio de sesión exitoso" });
        }
    }
}