namespace Domain.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> GuardarArchivo(IFormFile archivo);
    }
}