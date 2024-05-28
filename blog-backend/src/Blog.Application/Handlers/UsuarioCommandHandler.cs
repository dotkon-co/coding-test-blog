using Blog.Application.Commands.UsuarioCommands;
using Blog.Application.Services;
using Blog.Core.Mensagens.Commands;
using Blog.Core.Utilitarios.Conversores;
using Blog.Domain.Entidades.ControleAcesso;
using Blog.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.Handlers
{
    public class UsuarioCommandHandler :
        ICommandHandler<CriarUsuarioCommand>,
        ICommandHandler<AutenticarUsuarioCommand>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Perfil> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioCommandHandler(
            ITokenService tokenService,
            UserManager<Usuario> userManager,
            RoleManager<Perfil> roleManager,
            SignInManager<Usuario> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<CommandResult> Handler(CriarUsuarioCommand command)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(command.Email);
            if (usuarioExistente is not null) return new CommandResult(false, "E-mail já cadastrado.", command);

            var perfilUsuario = await _roleManager.FindByNameAsync(UsuarioPerfilEnum.Usuario.ObterDescricaoEnum());
            if(perfilUsuario is null) return new CommandResult(false, "Perfil usuário não encontrado.", command);

            var usuario = new Usuario(command.Nome, command.Email, null, perfilUsuario.Id);

            await _userManager.CreateAsync(usuario, command.Senha);

            return new CommandResult(true, "Usuário registrado com sucesso.", usuario);
        }

        public async Task<CommandResult> Handler(AutenticarUsuarioCommand command)
        {
            var resultado = await _signInManager.PasswordSignInAsync(command.Email, command.Senha, false, false);
            if (!resultado.Succeeded) return new CommandResult(false, "E-mail e/ou senha inválidos", command);

            var usuario = await _userManager.FindByEmailAsync(command.Email);
            if(usuario is null) return new CommandResult(false, "E-mail e/ou senha inválidos", command);

            var usuarioPerfis = await _userManager.GetRolesAsync(usuario);
            if (usuarioPerfis is null) return new CommandResult(false, "Usuário sem perfil para criação de token", usuario);

            var token = _tokenService.GerarToken(usuario.Id, usuario.Email!, usuarioPerfis);

            return new CommandResult(true, "Usuário validado com sucesso", new
            {
                usuario,
                usuarioPerfil = usuarioPerfis.FirstOrDefault(),
                token
            });
        }
    }
}
