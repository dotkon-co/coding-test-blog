using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;
using BlogSimples.Web.Service.Interfaces;
using Microsoft.Extensions.Hosting;

namespace BlogSimples.Web.Service
{
    public class PostagemService : IPostagemService
    {
        private readonly IPostagemRepository _postagemRepository;

        public PostagemService(IPostagemRepository postagemRepository)
        {
            _postagemRepository = postagemRepository;
        }

        public async Task AlterarAsync(Postagem post)
        {
            await _postagemRepository.UpdatePostagemAsync(post);
        }

        public async Task<Postagem> BuscarIdAsync(int postId)
        {
            return await _postagemRepository.GetByIdAsync(postId);
        }

        public async Task DeletarAsync(Postagem post)
        {
            await _postagemRepository.DeletePostagemAsync(post);
        }

        public async Task<int> GravarAsync(Postagem post)
        {
            return await _postagemRepository.AddPostagemAsync(post);
        }

        public async Task<IEnumerable<Postagem>> ListarAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Postagem>> ListarPostUserIdAsync(int postId)
        {
            return await _postagemRepository.GetPostagensIdAsync(postId);
        }
    }
}
