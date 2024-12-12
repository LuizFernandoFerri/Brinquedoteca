using BrinquedotecaPUC.Web.CrossCutting.Enumerators;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Web.Mvc.Html;

namespace BrinquedotecaPUC.Web.Controllers
{
    public class ClienteController : Controller
    {

        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            return GetAll(new ClienteFiltroViewModel());
        }

        public ActionResult Novo()
        {
            LimpaTempVars();
            ClienteViewModel cliente = new ClienteViewModel();
            cliente.IsEdit = false;
            ListaEstados();
            return View("FormCliente", cliente);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            LimpaTempVars();

            ClienteViewModel cliente = new ClienteViewModel();

            try
            {
                cliente = _clienteService.ObterCliente(id);
                
                if (!cliente.ResultValidation.IsValid)
                {
                    string erros = string.Empty;

                    foreach (var item in cliente.ResultValidation.Erros)
                    {
                        erros += item.Message;
                    }
                    
                    TempData["ModalError"] = erros;
                    return RedirectToAction("Index");
                }

                cliente.IsEdit = true;

                ListaEstados();
            }
            catch (Exception ex)
            {
                TempData["ModalError"] = "Erro ao recuperar cliente. Por favor, tente novamente.";
                return RedirectToAction("Index");
            }

            return View("FormCliente", cliente);
        }

        public ActionResult GetAll(ClienteFiltroViewModel filtroViewModel, int pageNumber = 1, int pageSize = 10)
        {
            var resultList = _clienteService.ListarClientes(pageNumber, pageSize, filtroViewModel);
            return View("Index", resultList);
        }

        [HttpGet]
        public ActionResult ListaClientes(ClienteFiltroViewModel filtroViewModel, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var resultList = _clienteService.ListarClientes(pageNumber, pageSize, filtroViewModel);

                // Se a chamada for AJAX, retorne apenas a partial view da grid
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("GridCliente", resultList);
                }

                // Caso contrário, retorne a view principal
                return View("Index", resultList);
            }
            catch (Exception e)
            {
                var msg = e.Message + " - " + (e.InnerException != null ? e.InnerException.Message : "");
                return Json(new { success = false, aErro = msg });
            }
        }

        [HttpPost]
        public ActionResult Salvar(ClienteViewModel cliente)
        {
            LimpaTempVars();

            ClienteViewModel clienteCadastrado = new ClienteViewModel();

            try
            {
                clienteCadastrado = _clienteService.Salvar(cliente);

                if (!clienteCadastrado.ResultValidation.IsValid)
                {
                    string erros = string.Empty;
                    
                    clienteCadastrado.ResultValidation.Erros.ToList().ForEach(x => erros = erros + x.Message + Environment.NewLine);

                    TempData["ModalError"] = erros;
                    return View("FormCliente", clienteCadastrado);
                }
                else
                {
                    ListaEstados();
                    TempData["ModalSuccess"] = "Usuário salvo com sucesso!";
                    return View("FormCliente", clienteCadastrado);
                }
            }
            catch (Exception ex) 
            {
                TempData["ModalError"] = ex.Message;
                return View("FormCliente", cliente);
            }
        }

        public async Task<ActionResult> Deletar(int id)
        {
            try
            {
                var result = await Task.Run(() => _clienteService.ExcluirCliente(id));

                if (result.IsValid == false)
                {
                    return NotFound($"Não foi possível deletar o cliente.");
                }

                return Ok("Cliente deletado com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        private void ListaEstados()
        {
            ViewBag.Estados = CrossCutting.Enumerators.EnumHelper.GetEnumSelectList<EstadosEnum>();
        }

        private void LimpaTempVars()
        {
            TempData["ModalError"] = null;
            TempData["ModalSuccess"] = null;
        }

        [HttpGet]
        public IActionResult BuscarUsuarios(string termo)
        {
            var result = _clienteService.ListaCodUsuarioLookup(termo.Replace("%", ""));

            var usuarios = result
                .Where(u => u.CodUsuario.Contains(termo))
                .Select(u => new { id = u.Id, nome = u.Nome })
                .ToList();

            return Json(usuarios);
        }
    }
}
