using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult UsuarioHome()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("LoginUsuario", "Login");
        }
    }
}
