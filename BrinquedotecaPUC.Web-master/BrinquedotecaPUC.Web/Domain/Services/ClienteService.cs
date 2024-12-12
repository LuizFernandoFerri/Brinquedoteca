using AutoMapper;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Domain.ValueObjects;
using BrinquedotecaPUC.Web.Models.Cliente;

namespace BrinquedotecaPUC.Web.Domain.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepositoryLeitura _clienteRepositoryLeitura;
        private readonly IClienteRepositoryEscrita _clienteRepositoryEscrita;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepositoryLeitura clienteRepositoryLeitura, IClienteRepositoryEscrita clienteRepositoryEscrita, 
            IUnitOfWork unitOfWork, IMapper mapper)
            : base(clienteRepositoryLeitura, clienteRepositoryEscrita)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clienteRepositoryLeitura = clienteRepositoryLeitura;
            _clienteRepositoryEscrita = clienteRepositoryEscrita;
        }

        public int ObterTotalClientes(string cpf, string nome, int idRec)
        {
            return _clienteRepositoryLeitura.ObterTotalClientes(cpf, nome, idRec);
        }

        public ClienteGridViewModel ListarClientes(int pageNumber, int pageSize, ClienteFiltroViewModel filtroCliente)
        {
            ClienteGridViewModel resultListaClientes = new ClienteGridViewModel();
            var result = _clienteRepositoryLeitura.ListarClientes(pageNumber, pageSize, filtroCliente.CodUsuario, filtroCliente.Cpf, filtroCliente.Nome, filtroCliente.IdRec);

            if (result != null && result.Any())
            {
                // Obter o total de clientes
                resultListaClientes.TotalClientes = _clienteRepositoryLeitura.ObterTotalClientes(filtroCliente.Cpf, filtroCliente.Nome, filtroCliente.IdRec);
                // Realizar o mapeamento
                resultListaClientes.Clientes = _mapper.Map<List<Cliente>, List<ClienteViewModel>>(result);
            }
            else
            {
                resultListaClientes.ResultValidadion ??= new ValidationResult();
                resultListaClientes.ResultValidadion.AdicionarErro(new ValidationError("Nenhum cliente encontrado."));
            }

            return resultListaClientes;
        }

        public ClienteViewModel ObterCliente(int id)
        {
            var result = _clienteRepositoryLeitura.LocalizaCliente(id);

            if (result != null)
            {
                var clienteViewModel = _mapper.Map<Cliente, ClienteViewModel>(result);
                clienteViewModel.CpfFormatado = $"{result.Cpf.Substring(0, 3)}.{result.Cpf.Substring(3, 3)}.{result.Cpf.Substring(6, 3)}-{result.Cpf.Substring(9, 2)}";
                return clienteViewModel;
            }
            else
            {
                ClienteViewModel resultErro = new ClienteViewModel();
                resultErro.ResultValidation.AdicionarErro(new ValidationError("Não foi possível localizar o cliente com o ID informado."));
                return resultErro;
            }
        }

        public ClienteViewModel Salvar(ClienteViewModel clienteEntity)
        {
            var resultValidation = new ClienteViewModel();

            try
            {
                var cliente = _mapper.Map<ClienteViewModel, Cliente>(clienteEntity);

                if (cliente != null)
                {
                    cliente.Cpf = clienteEntity.CpfFormatado.Replace(".", string.Empty).Replace("-", string.Empty);
                    if (cliente.Cpf != null)
                    {
                        if (!IsValidCPF(cliente.Cpf))
                            resultValidation.ResultValidation.AdicionarErro(new ValidationError("O CPF é inválido."));
                    }

                    if (cliente.Cep == null)
                        resultValidation.ResultValidation.AdicionarErro(new ValidationError("O CEP é obrigatório."));

                    if (string.IsNullOrEmpty(cliente.Logradouro) || string.IsNullOrEmpty(cliente.Numero))
                        resultValidation.ResultValidation.AdicionarErro(new ValidationError("O Número é obrigatório."));

                    if (resultValidation.ResultValidation.IsValid == false)
                    {
                        return resultValidation;
                    }
                    else
                    {
                        if (clienteEntity.IsEdit == true)
                        {
                            _clienteRepositoryEscrita.Update(cliente);
                        }
                        else
                        {
                            _clienteRepositoryEscrita.Add(cliente);
                        }

                        _unitOfWork.Commit();

                        resultValidation = _mapper.Map<Cliente, ClienteViewModel>(_clienteRepositoryLeitura.GetByKeyReadOnly(cliente.Cpf));
                    }
                }
                else
                {
                    resultValidation.ResultValidation.AdicionarErro(new ValidationError("Cliente não encontrado."));
                }
            }
            catch (Exception ex)
            {
                resultValidation.ResultValidation.AdicionarErro(new ValidationError($"Não foi possível salvar os dados do cliente. Motivo: {ex.Message}"));
            }

            return resultValidation;
        }

        private static bool IsValidCPF(string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim().Replace(".", string.Empty).Replace("-", string.Empty);

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public ValidationResult ExcluirCliente(int idRec)
        {
            var resultValidation = new ValidationResult();
            var cliente = _clienteRepositoryLeitura.GetByKey(idRec);

            if (cliente != null && cliente.CodUsuario != null)
            {
                var result = _clienteRepositoryLeitura.VerificaSePossuiMovimento(cliente.CodUsuario);
                
                if (!result)
                {
                    _clienteRepositoryEscrita.Remover(cliente);
                    _unitOfWork.Commit();
                }
                else
                {
                    resultValidation.AdicionarErro(new ValidationError("Cliente não pode ser excluído, pois ele possui movimentações na plataforma."));
                }
            }
            else
            {
                resultValidation.AdicionarErro(new ValidationError("Cliente não encontrado."));
                
            }

            return resultValidation;
        }

        public List<ClienteViewModel> ListaCodUsuarioLookup(string term)
        {
            var result = _clienteRepositoryLeitura.ListaCodUsuarioLookup(term);

            if (result != null && result.Any())
            {
                return _mapper.Map<List<Cliente>, List<ClienteViewModel>>(result);
            }
            else
            {
                return new List<ClienteViewModel>();
            }
        }
    }
}
