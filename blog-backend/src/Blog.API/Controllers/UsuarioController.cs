using Blog.Application.Commands.UsuarioCommands;
using Blog.Application.Handlers;
using Blog.Core.Mensagens.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioCommandHandler _usuarioCommandHandler;

        public UsuarioController([FromServices] UsuarioCommandHandler usuarioCommandHandler)
        {
            _usuarioCommandHandler = usuarioCommandHandler;
        }

        [HttpPost]
        [Route("criar")]
        [AllowAnonymous]
        public async Task<ActionResult<CommandResult>> CriarUsuario([FromBody] CriarUsuarioCommand command)
        {
            if (!command.IsValid) return BadRequest(command);

            var resultado = await _usuarioCommandHandler.Handler(command);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<CommandResult>> AutenticarUsuario([FromBody] AutenticarUsuarioCommand command)
        {
            if (!command.IsValid) return BadRequest(command);

            var resultado = await _usuarioCommandHandler.Handler(command);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
