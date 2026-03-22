namespace Domain
{
    public interface IClothingService
    {
        Task<object> AgregarPrenda(IFormFile imagen, string nombre);
        Task EliminarPrenda(int id);
        Task<IEnumerable<Clothing>> ObtenerPrendas(int userId);
    }
}