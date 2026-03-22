using Domain;

namespace Infrastructure.Services
{
    public class ClothingService : IClothingService
{
    private readonly IClothingRepository repository;

    public ClothingService(IClothingRepository repository)
    {
        this.repository = repository;
    }

    public async Task<object> AgregarPrenda(IFormFile imagen, string nombre)
{
    if (imagen == null || imagen.Length == 0)
        return new { Success = false, Message = "No se envió imagen" };

    var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
    var carpeta = Path.Combine("wwwroot", "closet");

    Directory.CreateDirectory(carpeta);

    var ruta = Path.Combine(carpeta, nombreArchivo);

    using (var stream = new FileStream(ruta, FileMode.Create))
    {
        await imagen.CopyToAsync(stream);
    }

    var clothing = new Clothing
    {
        UserId = 1,
        Name = nombre,
        ImageUrl = "/closet/" + nombreArchivo,
        Category = "General"
    };

    var id = await repository.AgregarPrenda(clothing);

    return new 
    {
        Success = true,
        Message = "Prenda agregada exitosamente",
        Url = clothing.ImageUrl,
        Id = id
    };
}

    public async Task EliminarPrenda(int id)
    {
        await repository.EliminarPrenda(id);
    }

    public async Task<IEnumerable<Clothing>> ObtenerPrendas(int userId)
    {
        return await repository.ObtenerPrendasPorUsuario(userId);
    }
}
}
