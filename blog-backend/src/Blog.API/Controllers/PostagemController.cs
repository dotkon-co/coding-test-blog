using Blog.Application.Commands.PostagemCommands;
using Blog.Application.Handlers;
using Blog.Application.Queries.PostagemQueries;
using Blog.Application.Queries.PostagemQueries.ViewModels;
using Blog.Core.Mensagens.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("v1/postagem")]
    public class PostagemController : ControllerBase
    {
        private readonly PostagemCommandHandler _postagemCommandHandler;
        private readonly IPostagemQuery _postagemQuery;

        public PostagemController(
            [FromServices] PostagemCommandHandler postagemCommandHandler,
            IPostagemQuery postagemQuery)
        {
            _postagemCommandHandler = postagemCommandHandler;
            _postagemQuery = postagemQuery;
        }

        [HttpPost]
        [Route("postar")]
        public async Task<ActionResult<CommandResult>> PostarConteudo([FromBody] PostarConteudoCommand command)
        {
            if (!command.IsValid) return BadRequest(command);

            var resultado = await _postagemCommandHandler.Handler(command);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<ActionResult<CommandResult>> AlterarPostagem([FromBody] AlterarPostagemCommand command)
        {
            if (!command.IsValid) return BadRequest(command);

            var resultado = await _postagemCommandHandler.Handler(command);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpDelete]
        [Route("excluir")]
        public async Task<ActionResult<CommandResult>> ExcluirPostagem([FromBody] ExcluirPostagemCommand command)
        {
            if (!command.IsValid) return BadRequest(command);

            var resultado = await _postagemCommandHandler.Handler(command);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }

        [Route("todas")]
        [HttpGet]
        public async Task<IEnumerable<PostagemViewModel>> ObterTodasPostagens() =>
            await _postagemQuery.ObterTodasPostagens();
    }
}
