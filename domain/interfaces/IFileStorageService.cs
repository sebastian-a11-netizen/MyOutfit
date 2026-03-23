namespace Domain
{
    public interface IFileStorageService
    {
        Task<string> GuardarArchivo(IFormFile archivo);
    }
}