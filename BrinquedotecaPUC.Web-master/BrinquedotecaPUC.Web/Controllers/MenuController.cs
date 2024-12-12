using BrinquedotecaPUC.Web.Models.Usuario;
using Microsoft.AspNetCore.Mvc;


namespace BrinquedotecaPUC.Web.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
