using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace vue_expenses_api.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnv;

        public HomeController(
            IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public IActionResult Index()
        {
            return new PhysicalFileResult(
                System.IO.Path.Combine(
                    _hostingEnv.WebRootPath,
                    "index.html"),
                new MediaTypeHeaderValue("text/html")
            );
        }
    }
}