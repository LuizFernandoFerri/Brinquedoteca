using AutoMapper;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Domain.ValueObjects;
using BrinquedotecaPUC.Web.Infra.Data.Repositories;
using BrinquedotecaPUC.Web.Models.Cliente;
using BrinquedotecaPUC.Web.Models.Produto;
using NuGet.Protocol;

namespace BrinquedotecaPUC.Web.Domain.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private readonly IProdutoRepositoryLeitura _produtoRepositoryLeitura;
        private readonly IProdutoRepositoryEscrita _produtoRepositoryEscrita;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepositoryLeitura produtoRepositoryLeitura, IProdutoRepositoryEscrita produtoRepositoryEscrita, 
            IUnitOfWork unitOfWork, IMapper mapper)
            : base(produtoRepositoryLeitura, produtoRepositoryEscrita)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _produtoRepositoryLeitura = produtoRepositoryLeitura;
            _produtoRepositoryEscrita = produtoRepositoryEscrita;
        }

        public ProdutoViewModel GetProductById(int idProduto)
        {
            var result = _produtoRepositoryLeitura.GetProductByKey(idProduto);

            if (result != null)
            {
                var resultProduto = _mapper.Map<Produto, ProdutoViewModel>(result);
                return _mapper.Map<Produto, ProdutoViewModel>(result);
            }
            else
            {
                ProdutoViewModel resultErro = new ProdutoViewModel();
                resultErro.ResultValidation.AdicionarErro(new ValidationError("Não foi possível localizar o produto com o ID informado."));
                return resultErro;
            }
        }

        public ProdutoGridViewModel ProdutoList(int pageNumber, int pageSize, ProdutoFiltroViewModel filtroViewModel)
        {
            ProdutoGridViewModel resultProduto = new ProdutoGridViewModel();

            var result = _produtoRepositoryLeitura.ProdutoList(pageNumber, pageSize, filtroViewModel.CodProduto, filtroViewModel.Descricao, filtroViewModel.IdEstConservacao, filtroViewModel.IdProduto);

            if (result.Count() > 0)
            {
                resultProduto.Produtos = _mapper.Map<List<Produto>, List<ProdutoViewModel>>(result);
            }
            else
            {
                resultProduto.ResultValidadion ??= new ValidationResult();
                resultProduto.ResultValidadion.AdicionarErro(new ValidationError("Nenhum produto encontrado."));
            }

            return resultProduto;
        }

        public ProdutoViewModel Salvar(ProdutoViewModel prodViewModel)
        {
            var resultValidadion = new ProdutoViewModel();

            if (prodViewModel != null)
            {
                var produto = _mapper.Map<ProdutoViewModel, Produto>(prodViewModel);
                produto.UrlImagem = prodViewModel.Imagem.FullName;

                if (string.IsNullOrEmpty(produto.Descricao))
                {
                    resultValidadion.ResultValidation.AdicionarErro(new ValidationError("Um nome para o brinquedo/produto deve ser informado"));
                }
                else if (produto.IdCategoria <= 0 || produto.IdCategoria == null)
                {
                    resultValidadion.ResultValidation.AdicionarErro(new ValidationError("Um nome para o brinquedo/produto deve ser informado"));
                }
                else if (produto.IdEstConservacao <= 0 || produto.IdEstConservacao == null)
                {
                    resultValidadion.ResultValidation.AdicionarErro(new ValidationError("Informe o estado de conservação do brinquedo/produto."));
                }
                else if (produto.IdFaixaEtaria <= 0 || produto.IdFaixaEtaria == null)
                {
                    resultValidadion.ResultValidation.AdicionarErro(new ValidationError("Informe a faixa etária do brinquedo/produto."));
                }

                if (resultValidadion.ResultValidation.IsValid == false)
                {
                    return resultValidadion;
                }
                else
                {
                    if (prodViewModel.IsEdit == true)
                    {
                        _produtoRepositoryEscrita.Update(produto);
                    }
                    else
                    {
                        _produtoRepositoryEscrita.Add(produto);
                    }

                    _unitOfWork.Commit();

                    resultValidadion = _mapper.Map<Produto, ProdutoViewModel>(_produtoRepositoryLeitura.GetProductByKey((int)produto.IdProduto));
                }
            }
            else
            {
                resultValidadion.ResultValidation.AdicionarErro(new ValidationError("Produto não informado."));
            }

            return resultValidadion;
        }
    }
}
