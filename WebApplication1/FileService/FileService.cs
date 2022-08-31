using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebApplication1.FileService
{
    public class FileService : IFileService
    {
        private readonly string _directory;
        private readonly string _folderName;

        private FileDbContext _context;

        public FileService(FileDbContext context, string directory = @"D:", string folderName = "photos")
        {
            _directory = directory;
            _folderName = folderName;
            _context = context;
        }


        public async Task UpoloadFileAsync(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("file not found");
            
            var uploadFolderPath = Path.Combine(_directory, _folderName);

            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = id.ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(uploadFolderPath, fileName);


            using (var stream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(stream);
            }

            var fi = new File();
            fi.FileName = fileName;
            fi.CreatedOn = DateTime.Now.ToString();
            fi.ModifiedOn = DateTime.Now.ToString();
            fi.FileId = id;
            fi.ContentType = file.ContentType;
            await Add(fi);
        }
        
        public async Task<(byte[] bytes, string fileName)> Get(Guid id)
        {
            var file = await GetFileById(id);
            string path = Path.Combine(_directory, _folderName, file.FileName);

            byte[] bytes = System.IO.File.ReadAllBytes(path);            

            return (bytes, file.FileName);
        }

        public async Task Add(File file)
        {
                await _context.Files.AddAsync(file);
                await _context.SaveChangesAsync();
        }

        public async Task<File> GetFileById(Guid id)
        {
           var file = await _context.Files.FindAsync(id);
            return file;
        }

    }
}
