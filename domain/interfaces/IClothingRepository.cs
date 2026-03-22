using Domain;

public interface IClothingRepository
{
    Task<int> AgregarPrenda(Clothing clothing);
    Task EliminarPrenda(int id);
    Task<IEnumerable<Clothing>> ObtenerPrendasPorUsuario(int userId);
}