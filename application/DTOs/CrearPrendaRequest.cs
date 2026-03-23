namespace Application.DTOs
{
    public class CrearPrendaRequest
    {
        public IFormFile Imagen { get; set; }
        public string Nombre { get; set; }
    }
}