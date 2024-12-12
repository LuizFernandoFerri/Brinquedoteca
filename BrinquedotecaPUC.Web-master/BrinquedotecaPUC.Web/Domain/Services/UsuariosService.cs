using AutoMapper;
using BrinquedotecaPUC.Web.Domain.Entities;
using BrinquedotecaPUC.Web.Domain.Interfaces;
using BrinquedotecaPUC.Web.Domain.Interfaces.Repositories;
using BrinquedotecaPUC.Web.Domain.ValueObjects;
using BrinquedotecaPUC.Web.Models.Usuario;

namespace BrinquedotecaPUC.Web.Domain.Services
{
    public class UsuariosService : ServiceBase<Usuario>, IUsuariosService
    {
        public IUsuarioRepositoryEscrita? EscritaRepository { get { return _EscritaRepository as IUsuarioRepositoryEscrita; } }
        public IUsuarioRepositoryLeitura? LeituraRepository { get { return _LeituraRepository as IUsuarioRepositoryLeitura; } }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuariosService(IUsuarioRepositoryEscrita usuarioRepositoryEscrita, IUsuarioRepositoryLeitura usuarioRepositoryLeitura, IUnitOfWork unitOfWork, IMapper mapper)
            : base(usuarioRepositoryLeitura, usuarioRepositoryEscrita)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UsuarioViewModel SalvarUsuario(UsuarioViewModel usuarioViewModel)
        {
            ValidationResult result = new ValidationResult();

            Usuario usuario = _mapper.Map<Usuario>(usuarioViewModel);

            try
            {
                if (usuarioViewModel is null)
                {
                    return null;
                }

                #region .: Validações se já existe usuário :.

                Usuario usuarioJaExiste = LeituraRepository.LocalizaUsuario(usuario.CodUsuario, usuario.Email);

                if (usuarioJaExiste != null)
                {
                    usuario.ResultValidation.AdicionarErro(new ValidationError("Usuário já cadastrado."));
                    return _mapper.Map<UsuarioViewModel>(usuario);
                }

                #endregion .: Validações se já existe usuário :.

                var hash = SecurePasswordHasher.Hash(usuario.Password);

                usuario.Password = hash;

                if (usuarioViewModel.IsEdit == true)
                {
                    EscritaRepository.Update(usuario);
                }
                else
                {
                    EscritaRepository.Add(usuario);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                usuario.ResultValidation.AdicionarErro(new ValidationError($"Falha: {nameof(UsuariosService)}; {nameof(SalvarUsuario)}; {ex.Message};"));
            }

            return _mapper.Map<UsuarioViewModel>(usuario);
        }

        public async Task<List<UsuarioGridViewModel>?> GetAllUsers()
        {
            try
            {
                List<Usuario>? listaUsuarios = LeituraRepository.ListarUsuarios();

                if (listaUsuarios is null)
                    return null;

                return await Task.FromResult(_mapper.Map<List<UsuarioGridViewModel>>(listaUsuarios));
            }
            catch (Exception ex)
            {
                //usuarioViewModel.ResultValidation.AdicionarErro(new ValidationError($"Falha: {nameof(UsuariosService)}; {nameof(CreateUser)}; {ex.Message};"));
                throw;
            }
        }

        public async Task<UsuarioViewModel> GetByKeyReadOnly(string codUsuario)
        {
            UsuarioViewModel result = new UsuarioViewModel();

            try
            {
                Usuario? usuario = LeituraRepository.GetByKeyReadOnly(codUsuario);

                if (usuario != null)
                {
                    result = await Task.FromResult(_mapper.Map<UsuarioViewModel>(usuario));
                }
                else
                {
                    result.ResultValidation.AdicionarErro(new ValidationError("Usuário não localizado."));
                }

                return result;
            }
            catch (Exception ex) 
            {
                result.ResultValidation.AdicionarErro(new ValidationError($"Erro: {ex}"));
                return result;
            }
        }

        public async Task<UsuarioViewModel?> Login(string userName, string userPassaword)
        {
            Usuario usuario = new Usuario();

            // É passado o userName duas vezes, pois o método LocalizaUsuario espera dois parâmetros, um para pesquisar por código de usuário e outro por e-mail.
            // Neste caso, como o endpoint é de login, o userName é o código do usuário e também pode ser o e-mail.
            usuario = LeituraRepository.LocalizaUsuario(userName, userName);

            if (usuario is null)
            {
                usuario.CodUsuario = userName;
                usuario.ResultValidation.AdicionarErro(new ValidationError("Usuário não localizado."));
                return _mapper.Map<UsuarioViewModel>(usuario);
            }

            if (!SecurePasswordHasher.Verify(userPassaword, usuario.Password))
            {
                usuario.SenhaInvalida = true;
            }

            return _mapper.Map<UsuarioViewModel>(usuario);
        }
    }
}
