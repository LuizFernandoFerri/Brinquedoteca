using BrinquedotecaPUC.Web.CrossCutting.Enumerators;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Models.Produto;
using Microsoft.AspNetCore.Mvc;

namespace BrinquedotecaPUC.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IWebHostEnvironment _env;

        public ProdutoController(IProdutoService produtoService, IWebHostEnvironment env)
        {
            _env = env;
            _produtoService = produtoService;
        }
        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            return GetAll(new ProdutoFiltroViewModel());
        }

        public ActionResult GetAll(ProdutoFiltroViewModel filtroViewModel, int pageNumber = 1, int pageSize = 10)
        {
            var resultList = _produtoService.ProdutoList(pageNumber, pageSize, filtroViewModel);
            return View("Index", resultList);
        }

        // GET: ProdutosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProdutosController/Create
        public ActionResult Create()
        {
            LimpaTempVars();
            ProdutoViewModel produto = new ProdutoViewModel();
            produto.IdProduto = 0;
            produto.IsEdit = false;
            //MontaSelectList();
            return View("FormProduto", produto);
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutosController/Edit/5
        public ActionResult Editar(int id)
        {
            LimpaTempVars();

            ProdutoViewModel produto = new ProdutoViewModel();

            try
            {
                produto = _produtoService.GetProductById(id);

                if (!produto.ResultValidation.IsValid)
                {
                    string erros = string.Empty;

                    foreach (var item in produto.ResultValidation.Erros)
                    {
                        erros += item.Message;
                    }

                    TempData["ModalError"] = erros;
                    return RedirectToAction("Index");
                }

                produto.IsEdit = true;

                //MontaSelectList();
            }
            catch (Exception ex)
            {
                TempData["ModalError"] = "Erro ao recuperar produto selecionado. Por favor, tente novamente.";
                return RedirectToAction("Index");
            }

            return View("FormProduto", produto);
        }

        //[HttpPost("upload-imagem")]
        [HttpPost]
        public async Task<IActionResult> Salvar([FromForm] ProdutoViewModel produtoViewModel, [FromForm] IFormFile? imagem)
        {
            if (imagem == null || imagem.Length == 0)
                return BadRequest("Imagem inválida.");

            try
            {
                // Diretório onde as imagens serão salvas
                string uploadDir = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                // Nome único para a imagem
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
                string filePath = Path.Combine(uploadDir, uniqueFileName);

                // Salvar o arquivo no servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagem.CopyToAsync(fileStream);
                }

                // Caminho da URL para salvar no banco
                string imageUrl = $"/uploads/{uniqueFileName}";

                // Define a URL da imagem no ViewModel
                produtoViewModel.Imagem = new FileInfo(filePath);

                // Salva o caminho no banco
                //await _produtoRepository.SalvarUrlImagem(imageUrl);
                var resultSalvarProduto = _produtoService.Salvar(produtoViewModel);

                return Ok(new { message = "Imagem salva com sucesso.", imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        protected void MontaSelectList()
        {
            ViewBag.SelectFaixaEtaria = EnumHelper.GetEnumDescriptonSelectList<FaixaEtariaEnum>();
            ViewBag.SelectCategorias = EnumHelper.GetEnumDescriptonSelectList<CategoriasEnum>();
            ViewBag.SelectEstadoConservacao = EnumHelper.GetEnumDescriptonSelectList<EstadoConservacaoEnum>();
            ViewBag.SelectStatusProduto = EnumHelper.GetEnumDescriptonSelectList<StatusProduto>();
        }

        private void LimpaTempVars()
        {
            TempData["ModalError"] = null;
            TempData["ModalSuccess"] = null;
        }
    }
}
