using Microsoft.Extensions.Options;

namespace WebApplication1.FileService
{
    public class FileService : IFileService
    {
        private readonly string _directory;
        private readonly string _folderName;

        public FileService( string directory, string folderName)
        {
            _directory = directory;
            _folderName = folderName;
        }


        public async Task UpoloadFileAsync(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("file not found");
            
            var uploadFolderPath = Path.Combine(_directory, _folderName);

            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = $"{id}.jpg";
            var path = Path.Combine(uploadFolderPath, fileName);


            using (var stream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(stream);
            }
        }
        
        public async Task<byte[]> GetFile(string name)
        {

            string path = Path.Combine(_directory, _folderName, name);

            byte[] bytes = System.IO.File.ReadAllBytes(path);            

            return bytes;
        }
    }
}
