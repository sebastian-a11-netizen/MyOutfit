using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothingController : ControllerBase
    {
        private readonly IClothingService clothingService;

        public ClothingController(IClothingService clothingService)
        {
            this.clothingService = clothingService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AgregarPrenda([FromForm] IFormFile imagen, [FromForm] string nombre)
        {
            var resultado = await clothingService.AgregarPrenda(imagen, nombre);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPrenda(int id)
        {
            await clothingService.EliminarPrenda(id);
            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPrendas()
        {
            var prendas = await clothingService.ObtenerPrendas(1); 
            return Ok(prendas);
        }
    }
}