using Application.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClothingService
    {
        Task<PrendaResponse> AgregarPrenda(CrearPrendaRequest request);
        Task EliminarPrenda(int id);
        Task<IEnumerable<Clothing>> ObtenerPrendas(int userId);
    }
}