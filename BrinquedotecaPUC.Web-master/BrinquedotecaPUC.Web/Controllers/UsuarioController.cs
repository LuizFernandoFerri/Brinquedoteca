using AutoMapper;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Services;
using BrinquedotecaPUC.Web.Domain.ValueObjects;
using BrinquedotecaPUC.Web.Models.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrinquedotecaPUC.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariosService _userService;

        public UsuarioController(IConfiguration configuration, IUsuariosService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return await Listar();
        }

        public IActionResult Novo()
        {
            return View("FormUsuario", new UsuarioViewModel());
        }

        public async Task<IActionResult> Listar()
        {
            var usuarios = await _userService.GetAllUsers();

            if (usuarios == null)
                TempData["ModalError"] = "Usuário não encontrado.";
                //ModelState.AddModelError("ServerError", "Não foram encontrados registros de usuários");

            return View("Index", usuarios);
        }

        public async Task<IActionResult> Editar(string codUsuario)
        {
            var usuario = await _userService.GetByKeyReadOnly(codUsuario);

            if (usuario == null)
            {
                //ModelState.AddModelError("ServerError", "Usuário não encontrado.");
                TempData["ModalSuccess"] = "Usuário não encontrado.";
                return View("FormUsuario", new UsuarioViewModel());
            }

            usuario.IsEdit = true;

            return View("FormUsuario", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(UsuarioViewModel viewModel)
        {
            try
            {
                if (viewModel.Password != viewModel.ConfirmPassword)
                {
                    TempData["ModalError"] = "As senhas não conferem!";
                    //ModelState.AddModelError("ConfirmPassword", "As senhas não conferem!");
                    return View("FormUsuario", viewModel);
                }

                var result = _userService.SalvarUsuario(viewModel);

                if (result.ResultValidation.IsValid)
                {
                    TempData["UserName"] = viewModel.Email;
                    TempData["Password"] = viewModel.Password;
                    TempData["ModalSuccess"] = "Usuário salvo com sucesso!";
                    //ModelState.AddModelError("SuccessMessage", "Usuário salvo com sucesso!");
                }
                else
                {
                    TempData["ModalError"] = "As senhas não conferem!";
                    //ModelState.AddModelError("ServerError", "E-mail e/ou código de usuário já cadastrado para outro usuário.");
                }


                return View("FormUsuario", viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ServerError", $@"Erro no servidor: {ex}");
                return View("FormUsuario", viewModel);
            }
        }
    }
}
