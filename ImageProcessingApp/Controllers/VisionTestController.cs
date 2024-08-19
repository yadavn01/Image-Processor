using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Vision.V1;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;

namespace ImageProcessingApp.Controllers
{
    public class VisionTestController : Controller
    {
    private readonly IVisionService _visionService;

    public VisionTestController(IVisionService visionService)
    {
        _visionService = visionService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult HealthCheck()
    {
        return Ok("API is working");
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile imageFile)
    {
        var resultMessage = await _visionService.ProcessImageAsync(imageFile);
        ViewBag.Message = resultMessage;
        return View();
    }
}
}
