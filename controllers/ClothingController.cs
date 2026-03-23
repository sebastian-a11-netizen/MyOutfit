using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.DTOs;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothingController : ControllerBase
    {
        private readonly IClothingService service;

        public ClothingController(IClothingService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AgregarPrenda([FromForm] CrearPrendaRequest request)
        {
            var resultado = await service.AgregarPrenda(request);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPrenda(int id)
        {
            await service.EliminarPrenda(id);
            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPrendas()
        {
            var prendas = await service.ObtenerPrendas(1); 
            return Ok(prendas);
        }
    }
}