using Blog.Application.Queries.PostagemQueries.ViewModels;
using Blog.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Queries.PostagemQueries
{
    public class PostagemQuery : IPostagemQuery
    {
        private readonly IPostagemRepository _postagemRepository;

        public PostagemQuery(IPostagemRepository postagemRepository)
        {
            _postagemRepository = postagemRepository;
        }

        public async Task<IEnumerable<PostagemViewModel>> ObterTodasPostagens() => 
            await _postagemRepository.ObterTodasPostagens()
                .Select(postagem => new PostagemViewModel(
                    postagem.Id,
                    postagem.AutorId,
                    postagem.Autor.Nome,
                    postagem.Titulo,
                    postagem.Conteudo,
                    postagem.DataCriacao))
                .ToListAsync();
    }
}
