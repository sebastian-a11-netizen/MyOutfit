using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
    Task<User?> ObtenerUsuarioPorEmail(string email);
    Task CrearUsuario(User user);
    }
}