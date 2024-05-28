using Blog.Application.Queries.PostagemQueries.ViewModels;

namespace Blog.Application.Queries.PostagemQueries
{
    public interface IPostagemQuery
    {
        Task<IEnumerable<PostagemViewModel>> ObterTodasPostagens();
    }
}
