using Domain;

namespace Infraestructure.Services
{
    public class FileStorageService : IFileStorageService
{
    public async Task<string> GuardarArchivo(IFormFile archivo)
    {
        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
        var carpeta = Path.Combine("wwwroot", "closet");

        Directory.CreateDirectory(carpeta);

        var ruta = Path.Combine(carpeta, nombreArchivo);

        using (var stream = new FileStream(ruta, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        return "/closet/" + nombreArchivo;
    }
}
}