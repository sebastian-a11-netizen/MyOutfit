using Domain;

public interface IUserRepository
{
    Task<User?> ObtenerUsuarioPorEmail(string email);
    Task CrearUsuario(User user);
}