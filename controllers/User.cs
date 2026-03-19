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
        public async Task<IActionResult> CrearUsuario([FromForm] User user)
        {
            await userRepository.CrearUsuario(user);
            return Redirect("/index.html");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = await userRepository.ObtenerUsuarioPorEmail(email);

            if (user == null || user.Password != password)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(new { message = "Inicio de sesión exitoso" });
        }
    }
}