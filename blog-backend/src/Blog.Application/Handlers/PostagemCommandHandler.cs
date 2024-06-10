using Blog.Application.Commands.PostagemCommands;
using Blog.Application.Hubs;
using Blog.Application.Queries.PostagemQueries.ViewModels;
using Blog.Application.Repositories;
using Blog.Core.Mensagens.Commands;
using Blog.Core.Mensagens.EventHubs;
using Blog.Core.Utilitarios.UsuarioHttpContext;
using Blog.Domain.Entidades.Postagem;

namespace Blog.Application.Handlers
{
    public class PostagemCommandHandler :
        ICommandHandler<PostarConteudoCommand>,
        ICommandHandler<AlterarPostagemCommand>,
        ICommandHandler<ExcluirPostagemCommand>
    {
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IBlogHub _blogHub;
        private readonly IPostagemRepository _postagemRepository;

        public PostagemCommandHandler(
            IUsuarioLogado usuarioLogado,
            IBlogHub blogHub,
            IPostagemRepository postagemRepository)
        {
            _usuarioLogado = usuarioLogado;
            _blogHub = blogHub;
            _postagemRepository = postagemRepository;
        }

        public async Task<CommandResult> Handler(PostarConteudoCommand command)
        {
            var post = new Post(_usuarioLogado.ObterId(), command.Titulo, command.Conteudo);

            await _postagemRepository.AdicionarPostagem(post);

            var commit = await _postagemRepository.UnitOfWork.Commit();
            if (commit is false) return new CommandResult(false, "Ocorreu um erro ao adicionar postagem.", command);

            var postAdicionado = await _postagemRepository.ObterPostagemPorId(post.Id);

            var postagemViewModel = new PostagemViewModel(
                    postAdicionado.Id,
                    postAdicionado.AutorId,
                    postAdicionado.Autor.Nome,
                    postAdicionado.Titulo,
                    postAdicionado.Conteudo,
                    postAdicionado.DataCriacao);

            await _blogHub.PostagemAdicionadaEvent(new PostagemAdicionadaEventHub(postagemViewModel.PostId, postagemViewModel.AutorId,
                postagemViewModel.AutorNome, postagemViewModel.Titulo, postagemViewModel.Conteudo, postagemViewModel.DataHoraCriacao));

            return new CommandResult(true, "Postagem realizada com sucesso.", postagemViewModel);
        }

        public async Task<CommandResult> Handler(AlterarPostagemCommand command)
        {
            var post = await _postagemRepository.ObterPostagemPorIdTracking(command.PostId);

            post.AlterarPostagem(command.Titulo, command.Conteudo);

            _postagemRepository.AlterarPostagem(post);

            var commit = await _postagemRepository.UnitOfWork.Commit();
            if (commit is false) return new CommandResult(false, "Ocorreu um erro ao alterar postagem.", command);

            var postagemViewModel = new PostagemViewModel(
                    post.Id,
                    post.AutorId,
                    post.Autor.Nome,
                    post.Titulo,
                    post.Conteudo,
                    post.DataCriacao);

            await _blogHub.PostagemAlteradaEvent(new PostagemAlteradaEventHub(postagemViewModel.PostId, postagemViewModel.AutorId,
                postagemViewModel.AutorNome, postagemViewModel.Titulo, postagemViewModel.Conteudo, postagemViewModel.DataHoraCriacao));

            return new CommandResult(true, "Postagem atualizada com sucesso.", postagemViewModel);
        }

        public async Task<CommandResult> Handler(ExcluirPostagemCommand command)
        {
            var post = await _postagemRepository.ObterPostagemPorIdTracking(command.PostId);

            _postagemRepository.ExcluirPostagem(post);

            var commit = await _postagemRepository.UnitOfWork.Commit();
            if (commit is false) return new CommandResult(false, "Ocorreu um erro ao excluir postagem.", command);

            await _blogHub.PostagemExcluidaEvent(new PostagemExcluidaEventHub(command.PostId));

            return new CommandResult(true, "Postagem excluida com sucesso.", command);
        }
    }
}
