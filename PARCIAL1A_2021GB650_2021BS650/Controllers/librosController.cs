using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A_2021GB650_2021BS650.Controllers;

namespace PARCIAL1A_2021GB650_2021BS650.Controllers
{
    public class librosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
