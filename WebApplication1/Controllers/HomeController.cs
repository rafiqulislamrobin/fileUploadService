using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.FileService;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  IFileService _fileService;

        public HomeController(ILogger<HomeController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
           await  _fileService.UpoloadFileAsync(Guid.NewGuid(), file);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        { 
            //var files = await _fileService.GetFiles();
            return View();
        }

        public async Task<FileResult> GetFileById(Guid id)
        {
            var fileName = $"{id}.jpg";
            var x = await _fileService.GetFile(fileName);
            return File(x, "application/octet-stream", fileName);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}