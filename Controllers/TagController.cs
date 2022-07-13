using Microsoft.AspNetCore.Mvc;

namespace FourBlog.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }
    }
}
