using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Services;
using BrinquedotecaPUC.Web.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace BrinquedotecaPUC.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuariosService _userService;

        public LoginController(IConfiguration configuration, IUsuariosService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var loginViewModel = new LoginViewModel();

            if (TempData["UserName"] != null && TempData["Password"] != null)
            {
                loginViewModel.UserName = TempData["UserName"].ToString();
                loginViewModel.Password = TempData["Password"].ToString();
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Autenticacao(LoginViewModel userModel)
        {
            try
            {
                UsuarioViewModel? result = await _userService.Login(userModel.UserName, userModel.Password);

                if (result != null)
                {
                    if (result.ResultValidation.IsValid)
                    {
                        //var token = TokenService.GenerateToken(result);
                        return View("/Views/Menu/Index.cshtml");
                    }
                    else if (!result.ResultValidation.IsValid)
                    {
                        ModelState.AddModelError("ServerError", "Usuário e/ou senha inválidos.");
                    }
                    else if (result.SenhaInvalida == true)
                    {
                        ModelState.AddModelError("ServerError", "Usuário não encontrado.");
                    }
                }
                else
                {
                    ModelState.AddModelError("ServerError", "Erro no servidor. Contate o Administrador.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ServerError", "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }

            //return new { username = result.Nome, token };

            return View("Index", userModel);
        }
    }
}
