using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CrearUsuario([FromForm] User user)
        {
            await userService.CrearUsuario(user);
            return Redirect("/index.html");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = await userService.ObtenerUsuarioPorEmail(email);

            if (user == null || user.Password != password)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(new { message = "Inicio de sesión exitoso" });
        }
    }
}