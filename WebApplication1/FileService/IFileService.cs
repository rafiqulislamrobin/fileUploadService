namespace WebApplication1.FileService
{
    public interface IFileService
    {
        Task UpoloadFileAsync(Guid id, IFormFile file);
        Task<(byte[] bytes, string fileName)> Get(Guid id);
    }
}
