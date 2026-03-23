using Domain.Interfaces;
using Domain.Entities;
using Application.DTOs;

namespace Application.Services
{
    public class ClothingService : IClothingService
{
    private readonly IClothingRepository repository;
    private readonly IFileStorageService fileStorage;

    public ClothingService(IClothingRepository repository, IFileStorageService fileStorage)
    {
        this.repository = repository;
        this.fileStorage = fileStorage;
    }

    public async Task<PrendaResponse> AgregarPrenda(CrearPrendaRequest request)
    {
        if (request.Imagen == null || request.Imagen.Length == 0)
            return new PrendaResponse { Success = false, Message = "No se envió imagen" };

        var imageUrl = await fileStorage.GuardarArchivo(request.Imagen);

        var clothing = new Clothing
        {
            UserId = 1,
            Name = request.Nombre,
            ImageUrl = imageUrl,
            Category = "General"
        };

        var id = await repository.AgregarPrenda(clothing);

        return new PrendaResponse
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

